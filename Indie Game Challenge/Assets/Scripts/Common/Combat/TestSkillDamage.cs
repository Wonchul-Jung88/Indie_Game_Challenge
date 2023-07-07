using UnityEngine;

public class TestSkillDamage : MonoBehaviour
{
    public int damage = 10; // ボールが与えるダメージ量
    private TestSkill testSkill;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); // プレイヤーのタグが"Player"であることを想定
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
                Debug.Log("Ball has collided with an enemy.");
                testSkill.NotifyBallDestroyed(gameObject); // ボールが敵と衝突したことをTestSkillスクリプトに通知
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("enemyAI component is not attached to the enemy object.");
            }
        }
    }
}
