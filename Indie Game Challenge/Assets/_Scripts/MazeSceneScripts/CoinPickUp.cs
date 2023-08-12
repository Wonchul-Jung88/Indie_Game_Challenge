using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // If it does, destroy the coin and increase coin count in CoinManager
            CoinManager.Instance.GetCoin();
            Destroy(gameObject);
        }
    }
}
