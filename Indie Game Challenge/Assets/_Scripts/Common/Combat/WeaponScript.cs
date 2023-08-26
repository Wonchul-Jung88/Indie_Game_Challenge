using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject blastWave;
    public bool activated;

    private void OnCollisionEnter(Collision other)
    {
        if (activated)
        {
            bool hitGround = other.gameObject.layer == LayerMask.NameToLayer("WhatIsGround");
            bool hitEnemy = other.gameObject.GetComponent<EnemyAITutorial>() != null;

            if (hitGround || hitEnemy)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.up);

                GameObject spawnedObject = Instantiate(blastWave, transform.position, rotation);
                EnemyBlastWaveExplosion explosionComponent = spawnedObject.GetComponent<EnemyBlastWaveExplosion>();

                if (explosionComponent)
                {
                    explosionComponent.BlastDamage = GetComponent<EnemyAITutorial>().maxHealth;
                }

                Destroy(gameObject);
            }
        }
    }
}
