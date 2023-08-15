using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAITutorial : MonoBehaviour
{
    Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public int maxHealth;
    public GameObject WeaponBox;
    public GameObject DropLootPrefab;

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
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private EnemyAITutorial ai;
    private bool isKnockBack;
    private Vector3 randomTargetPoint;

    private float _speed;
    public float patrolRadius = 10.0f;
    
    private bool isUpdatingTarget = false; // �ǉ�: �R���[�`���̏�Ԃ�ǐՂ���t���O
    public EnemySpawner spawner;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the first GameObject with the "Player" tag

        _animator = GetComponent<Animator>();

        _isChasingHash = Animator.StringToHash("IsChasing");
        _isAttackingHash = Animator.StringToHash("IsAttacking");

        rb = GetComponent<Rigidbody>();

        GenerateRandomTargetPoint();
        StartCoroutine(UpdateTargetPointWithInterval());
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
        if (!isUpdatingTarget) // �ǉ�: �R���[�`�������삵�Ă��Ȃ��ꍇ�̂݃X�^�[�g
        {
            StartCoroutine(UpdateTargetPointWithInterval());
            isUpdatingTarget = true; // �ǉ�: �R���[�`���̏�Ԃ��X�V
        }

        //agent.speed = 1.0f;
        _speed = 1.0f;
        _animator.SetBool(_isChasingHash, false);
        _animator.SetBool(_isAttackingHash, false);

        if (!walkPointSet) SearchWalkPoint();

        MoveTowards(randomTargetPoint);
        //if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    void GenerateRandomTargetPoint()
    {
        randomTargetPoint = transform.position + new Vector3(
            Random.Range(-patrolRadius, patrolRadius),
            0,
            Random.Range(-patrolRadius, patrolRadius)
        );
    }

    public float updateInterval = 5.0f; // ���̎��ԊԊu�Ń����_���ȃ|�C���g�𐶐����܂��B

    // �R���[�`�����̍Ō�Ńt���O��false�ɐݒ�
    IEnumerator UpdateTargetPointWithInterval()
    {
        while (true)
        {
            GenerateRandomTargetPoint();

            // 3����6�̊ԂŃ����_���Ȏ��Ԃ�updateInterval�ɐݒ�
            updateInterval = Random.Range(3.0f, 6.0f);
            yield return new WaitForSeconds(updateInterval);
        }
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
        if (isUpdatingTarget)
        {
            StopCoroutine(UpdateTargetPointWithInterval());
            isUpdatingTarget = false; // �ǉ�: �R���[�`���̏�Ԃ��X�V
        }

        //agent.speed = 10.0f;
        _speed = 10.0f;
        _animator.SetBool(_isChasingHash, true);
        _animator.SetBool(_isAttackingHash, false);

        //agent.SetDestination(player.position);
        // �v���C���[�̕���������
        MoveTowards(player.position);
    }

    private void AttackPlayer()
    {
        //agent.speed = 0.0f;
        _speed = 0.0f;
        _animator.SetBool(_isAttackingHash, true);

        //Make sure enemy doesn't move
        //agent.SetDestination(transform.position);
        LookAtWithYFixed(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public float rotationSmoothTime = 0.3f; // �����̕�Ԃ̂��߂̃X���[�Y�^�C��
    private Vector3 currentVelocity; // SmoothDamp�̂��߂̌��݂̃x���V�e�B

    void MoveTowards(Vector3 target)
    {
        Vector3 lookPosition = target;
        lookPosition.y = transform.position.y; // Y���W�����݂̃I�u�W�F�N�g��Y���W�ɐݒ�

        Vector3 desiredDirection = (lookPosition - transform.position).normalized;
        desiredDirection.y = 0;

        // ������SmoothDamp�Ŋ��炩�ɂ���
        Vector3 smoothDirection = Vector3.SmoothDamp(transform.forward, desiredDirection, ref currentVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.LookRotation(smoothDirection);

        float actualMoveDistance = _speed * Time.deltaTime;
        float remainingDistanceToTarget = (target - transform.position).magnitude;

        // �^�[�Q�b�g�܂ł̋��������̃t���[���ł̈ړ��������Z���ꍇ�A���ۂ̋��������i��
        if (remainingDistanceToTarget < actualMoveDistance)
        {
            actualMoveDistance = remainingDistanceToTarget;
        }

        transform.position += smoothDirection * actualMoveDistance;

        // �^�[�Q�b�g�ɓ��B�����ꍇ�A�V���������_���ȃ^�[�Q�b�g�|�C���g�𐶐�
        if (remainingDistanceToTarget <= actualMoveDistance)
        {
            GenerateRandomTargetPoint();
        }
    }

    void LookAtWithYFixed(Transform target)
    {
        transform.LookAt(target);
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = 0;  // X���̉�]��0�ɐݒ�
        eulerAngles.z = 0;  // Z���̉�]��0�ɐݒ�
        transform.eulerAngles = eulerAngles;
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

        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        WeaponBox.SetActive(true);
        AttackBox.SetActive(false);

        for ( int i = 0; i < Random.Range(1,5); i++ )
        {
            Vector3 randomness = new Vector3(Random.Range(0f, 0.25f), Random.Range(0f, 0.25f), Random.Range(0, 0.25f));
            Instantiate(DropLootPrefab, transform.position + Vector3.up + randomness, Quaternion.identity);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.DecrementEnemyCount();
        }
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
