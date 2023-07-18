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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyAITutorial enemyAI = collision.gameObject.GetComponent<EnemyAITutorial>();

            if (enemyAI != null)
            {
                enemyAI.TakeDamageToDie(damage);

                // Instantiate an explosion at the ball's position
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                //Debug.Log("Ball has collided with an enemy.");
                testSkill.NotifyBallDestroyed(gameObject);

                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("enemyAI component is not attached to the enemy object.");
            }
        }
    }
}
