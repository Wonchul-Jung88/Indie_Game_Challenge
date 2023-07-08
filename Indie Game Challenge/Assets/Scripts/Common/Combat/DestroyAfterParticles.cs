using UnityEngine;

public class DestroyAfterParticles : MonoBehaviour
{
    private ParticleSystem[] particleSystems;

    private void Start()
    {
        // Get all particle systems attached to this game object (and its children)
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    private void Update()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            // If any of the particle systems is still alive, return
            if (ps.IsAlive())
            {
                return;
            }
        }

        // If none of the particle systems is alive anymore, destroy the game object
        Destroy(gameObject);
    }
}
