using UnityEngine;

public class SpiderWebAttack : MonoBehaviour
{
    public GameObject webPrefab; // Assign this in the inspector
    public float shootForce = 10f;
    public float webLifeTime = 5f; // Web will be destroyed after this many seconds
    public Transform webShooter; // Assign this in the inspector
    public float angle = 0f; // New public property for the angle

    public void ShootWeb()
    {
        // Create a new web at the webShooter's position.
        GameObject web = Instantiate(webPrefab, webShooter.position, Quaternion.identity);

        // Assume the web has a Rigidbody component and apply force to it.
        Rigidbody rb = web.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calculate the shoot direction based on the provided angle
            Vector3 shootDirection = Quaternion.Euler(0, 0, angle) * webShooter.forward;
            rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
        }

        // Destroy the web after a certain amount of time
        Destroy(web, webLifeTime);
    }
}
