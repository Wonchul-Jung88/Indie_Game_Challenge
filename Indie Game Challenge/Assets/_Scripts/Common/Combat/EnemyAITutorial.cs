using UnityEngine;
using UnityEngine.AI;

public class EnemyAITutorial : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public int maxHealth;
    public GameObject WeaponBox;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool isDead;

    Animator _animator;
    int _isChasingHash;
    int _isAttackingHash;

    //public GameObject explosionPrefab;
    public GameObject AttackBox;

    public float knockBackThreshold = 0.01f;
    public float rotationSpeed = 50f;

    private Rigidbody rb;
    private EnemyAITutorial ai;
    private bool isKnockBack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _isChasingHash = Animator.StringToHash("IsChasing");
        _isAttackingHash = Animator.StringToHash("IsAttacking");

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDead) return;
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        if (isKnockBack)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
        }
    }

    private void FixedUpdate()
    {
        if (isKnockBack && rb.velocity.magnitude <= knockBackThreshold)
        {
            isKnockBack = false;
        }
    }

    private void Patroling()
    {
        agent.speed = 1.0f;
        _animator.SetBool(_isChasingHash, false);
        _animator.SetBool(_isAttackingHash, false);

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.speed = 10.0f;
        _animator.SetBool(_isChasingHash, true);
        _animator.SetBool(_isAttackingHash, false);

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.speed = 0.0f;
        _animator.SetBool(_isAttackingHash, true);

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamageToDie(int damage)
    {
        health -= damage;
        _animator.SetTrigger("Hurt");
        Vector3 randomness = new Vector3(Random.Range(0f, 0.25f), Random.Range(0f, 0.25f), Random.Range(0, 0.25f));
        DamagePopUpGenerator.Instance.CreatePopUp(transform.position + Vector3.up + randomness, damage.ToString(), Color.yellow);

        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyKnockBack(Vector3 force, ForceMode mode)
    {
        rb.AddForce(force, mode);
        isKnockBack = true;
    }

    public void TakeDamageToDestroy(int damage)
    {
        health -= damage;

        if ( health <= 0 ) Invoke(nameof(DestroyEnemy), .5f);
    }

    void Die()
    {
        _animator.SetBool("IsDead", true);
        isDead = true;

        //this.enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        WeaponBox.SetActive(WeaponBox);
        GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        AttackBox.SetActive(false);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyAITutorial enemyAI = collision.gameObject.GetComponent<EnemyAITutorial>();

            if (enemyAI != null && enemyAI.gameObject != this)
            {
                enemyAI.TakeDamageToDie(maxHealth);
                //Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                var direction = collision.gameObject.transform.position - this.gameObject.transform.position;
                enemyAI.ApplyKnockBack(direction.normalized, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("enemyAI component is not attached to the enemy object.");
            }
        }
    }

    public void EnableAttackBox()
    {
        if(AttackBox != null) AttackBox.GetComponent<Collider>().enabled = true;
    }

    public void DisableAttackBox()
    {
        if (AttackBox != null) AttackBox.GetComponent<Collider>().enabled = false;
    }
}
