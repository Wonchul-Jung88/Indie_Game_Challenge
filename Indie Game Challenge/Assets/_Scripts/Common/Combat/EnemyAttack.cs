using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Vector3 knockbackDirection;
    private PlayerStateMachine player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerStateMachine>();
            if (player != null)
            {
                UpdateKnockbackDirection();
                StartCoroutine(player.ApplyKnockBack(knockbackDirection, 2.0f, 0.3f));
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            UpdateKnockbackDirection();
        }
    }

    void UpdateKnockbackDirection()
    {
        knockbackDirection = player.transform.position - transform.parent.position;
    }
}
