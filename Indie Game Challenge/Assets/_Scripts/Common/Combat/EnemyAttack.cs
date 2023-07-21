using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject enemyBody;
    private PlayerStateMachine player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerStateMachine>();
            if (player != null)
            {
                StartCoroutine(player.ApplyKnockBack(enemyBody.transform.forward, 2.0f, 0.3f));
            }
        }
    }
}
