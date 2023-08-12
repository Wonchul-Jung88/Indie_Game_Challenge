using UnityEngine;

public class TestSkillDamage : MonoBehaviour
{
    public int damage = 10;
    private TestSkill testSkill;
    public GameObject explosionPrefab;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        testSkill = player.GetComponent<TestSkill>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyAITutorial enemyAI = other.gameObject.GetComponent<EnemyAITutorial>();

            if (enemyAI != null && !enemyAI.isDead)
            {
                enemyAI.TakeDamageToDie(damage);

                // Instantiate an explosion at the ball's position
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                testSkill.NotifyBallDestroyed(gameObject);

                Destroy(gameObject);
            }
        }
    }

}
