using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class EquipWeapon : MonoBehaviour
{
    GameObject Weapon;
    Rigidbody WeaponRigidbody;
    NavMeshAgent WeaponNavMeshAgent;

    public Transform WeaponPoint, Player;
    public Rig WeaponRig;
    public float throwPower = 30;

    public bool slotFull; // Changed from public static to public

    // Update is called once per frame
    void Update()
    {
        //if (slotFull && Input.GetKey(KeyCode.F) && Weapon != null)
        //{
        //    //Drop();
        //    ThrowWeapon();
        //}
    }

    public void ThrowWeapon()
    {
        if (WeaponRigidbody != null) WeaponRigidbody.isKinematic = false;
        if (WeaponNavMeshAgent != null) WeaponNavMeshAgent.enabled = true;
        //WeaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        WeaponRigidbody.AddForce(Player.forward * throwPower + Player.up * 2, ForceMode.Impulse);
        WeaponRigidbody = null;
        slotFull = false;
        WeaponRig.weight = 0;
        WeaponPoint.DetachChildren();
    }

    void Drop()
    {
        slotFull = false;
        WeaponRig.weight = 0;
        WeaponPoint.DetachChildren();

        if (WeaponRigidbody != null)
        {
            //WeaponRigidbody.isKinematic = false;
        }

        if (WeaponNavMeshAgent != null)
        {
            WeaponNavMeshAgent.enabled = true;
        }
    }

    void Equip()
    {
        if (Weapon == null) return;
        
        slotFull = true;
        WeaponRig.weight = 1;
        Weapon.transform.SetParent(WeaponPoint);

        if (WeaponNavMeshAgent != null)
        {
            WeaponNavMeshAgent.enabled = false;
        }

        if (WeaponRigidbody != null)
        {
            //WeaponRigidbody.isKinematic = true;
        }

        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Weapon.transform.localScale = Vector3.one / 28.0f;
    }

    void OnTriggerStay(Collider other)
    {
        // Get the parent object of the collider that was hit
        Transform parentObject = other.transform.parent;

        if (parentObject != null)
        {
            // Check if the parent object has the EnemyAITutorial component
            if (parentObject.TryGetComponent<EnemyAITutorial>(out EnemyAITutorial enemyAI))
            {
                if (enemyAI.isDead)
                {
                    Weapon = parentObject.gameObject;
                    WeaponRigidbody = Weapon.GetComponent<Rigidbody>();
                    WeaponNavMeshAgent = Weapon.GetComponent<NavMeshAgent>();
                    if (Input.GetKey(KeyCode.E))
                    {
                        Equip();
                    }
                }
            }
        }
    }


    public void EnableDamage()
    {
        if ( Weapon != null && Weapon.GetComponent<EnemyAITutorial>().isDead ) {
            var collider = Weapon.GetComponent<Collider>();
            collider.enabled = true;
        }
    }

    public void DisableDamage()
    {
        if (Weapon != null && Weapon.GetComponent<EnemyAITutorial>().isDead)
        {
            var collider = Weapon.GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
