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

        // Log details about the direction and power of the throw
        Debug.Log("Player forward direction: " + Player.forward);
        Debug.Log("Player up direction: " + Player.up);
        Debug.Log("Throw power: " + throwPower);

        Vector3 forwardForce = Player.forward * throwPower;
        Vector3 upwardForce = Player.up * 10;
        Debug.Log("Forward force: " + forwardForce);
        Debug.Log("Upward force: " + upwardForce);

        Vector3 totalForce = forwardForce + upwardForce;
        Debug.Log("Total force: " + totalForce);

        WeaponPoint.DetachChildren();
        //WeaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        WeaponRigidbody.useGravity = false;
        WeaponRigidbody.AddForce(totalForce, ForceMode.Impulse);
        WeaponRigidbody = null;
        slotFull = false;
        WeaponRig.weight = 0;
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
