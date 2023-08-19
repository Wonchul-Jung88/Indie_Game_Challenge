using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject blastWave;
    public bool activated;
    private Rigidbody rb; // Rigidbody�̎Q�Ƃ�ǉ�

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (activated)
        {
            // �ʒu�Ƒ��x�̃f�o�b�O���O���o��
            Debug.Log($"Weapon Position: {transform.position}");
            Debug.Log($"Weapon Velocity: {rb.velocity}");
        }
    }

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
