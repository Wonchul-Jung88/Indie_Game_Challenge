using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeaponScript : MonoBehaviour
{

    public bool activated;

    public float rotationSpeed;

    void Update()
    {
        if (activated)
        {
            //transform.localEulerAngles += Vector3.up * rotationSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (activated)
        {
            // �n�ʂ������͓G�I�u�W�F�N�g�ɏՓ˂����ꍇ�̏���
            bool hitGround = other.gameObject.layer == LayerMask.NameToLayer("WhatIsGround");
            bool hitEnemy = other.gameObject.GetComponent<EnemyAITutorial>() != null;

            if (hitGround || hitEnemy)
            {
                // �����͈͓��̂��ׂẴI�u�W�F�N�g���擾
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);

                int enemiesAffected = 0;

                foreach (Collider hitCollider in hitColliders)
                {
                    EnemyAITutorial enemyAI = hitCollider.gameObject.GetComponent<EnemyAITutorial>();
                    if (enemyAI != null && !enemyAI.isDead)
                    {
                        enemiesAffected++;
                        enemyAI.TakeDamageToDie(enemyAI.maxHealth);
                        var direction = other.gameObject.transform.position - this.gameObject.transform.position;
                        enemyAI.ApplyKnockBack(direction.normalized, ForceMode.Impulse);
                    }
                }

                // ���̃I�u�W�F�N�g�̐e�v�f��j��
                Destroy(gameObject);
            }
        }
    }

}
