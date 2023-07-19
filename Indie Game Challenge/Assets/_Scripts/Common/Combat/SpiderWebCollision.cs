using UnityEngine;

public class SpiderWebCollision : MonoBehaviour
{
    private PlayerStateMachine player;
    public float knockbackIntensity = 2.0f;
    public float knockbackDuration = 0.3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerStateMachine>();
            if (player != null)
            {
                Vector3 knockbackDirection = player.transform.position - transform.position;
                StartCoroutine(player.ApplyKnockBack(knockbackDirection, knockbackIntensity, knockbackDuration));
            }
            else
            {
                Debug.Log("PlayerStateMachine component not found on Player");
            }
        }
        else
        {
            Debug.Log("Web collided with non-Player object");
        }

        // Destroy the web immediately upon collision
        Destroy(gameObject);
    }
}
