using UnityEngine;

public class SpiderWebAttack : MonoBehaviour
{
    public GameObject webPrefab; // Assign this in the inspector
    public float shootForce = 10f;
    public float webLifeTime = 5f; // Web will be destroyed after this many seconds
    public Transform webShooter; // Assign this in the inspector
    public float cooldownTime = 2f; // Time in seconds between web shots

    private float lastShootTime;

    public void ShootWeb() // Make this function public so it can be called from other scripts
    {
        // Only shoot a web if enough time has passed since the last web shot
        if (Time.time - lastShootTime >= cooldownTime)
        {
            // Create a new web at the webShooter's position.
            GameObject web = Instantiate(webPrefab, webShooter.position, Quaternion.identity);

            // Assume the web has a Rigidbody component and apply force to it.
            Rigidbody rb = web.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 shootDirection = webShooter.forward;
                rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
            }

            // Destroy the web after a certain amount of time
            Destroy(web, webLifeTime);

            // Update the last shoot time
            lastShootTime = Time.time;
        }
    }
}
