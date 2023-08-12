using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject enemyBody;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine player))
        {
            player.LoseCoin();
            StartCoroutine(player.ApplyKnockBack(enemyBody.transform.forward, 2.0f, 0.3f));
        }
    }
}
