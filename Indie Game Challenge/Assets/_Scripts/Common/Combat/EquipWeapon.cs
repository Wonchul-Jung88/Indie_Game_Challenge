using System;
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

    WeaponScript _weaponScript;
    //public GameObject _tempWeapon;


    private void Start()
    {
        //_weaponScript = _tempWeapon.GetComponent<WeaponScript>();
        //WeaponRigidbody = _tempWeapon.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.F))
        //{
        //    ThrowWeapon2();
        //}
    }

    public void ThrowWeapon()
    {
        slotFull = false;
        _weaponScript.activated = true;
        WeaponRigidbody.isKinematic = false;
        WeaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        //if (WeaponNavMeshAgent != null) WeaponNavMeshAgent.enabled = true;
        Weapon.transform.parent = null;
        //Weapon.transform.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
        Weapon.transform.position += transform.right / 5;
        WeaponRigidbody.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode.Impulse);

        //WeaponRigidbody = null;
        WeaponRig.weight = 0;
    }

    public void ThrowWeapon2()
    {
        slotFull = false;
        _weaponScript.activated = true;
        WeaponRigidbody.isKinematic = false;
        WeaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        //_tempWeapon.transform.parent = null;
        //_tempWeapon.transform.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
        //_tempWeapon.transform.position += transform.right / 5;
        WeaponRigidbody.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode.Impulse);
    }

    void Equip( GameObject _weapon )
    {
        Weapon = _weapon;
        if (Weapon == null) return;
        WeaponRigidbody = Weapon.GetComponent<Rigidbody>();
        WeaponNavMeshAgent = Weapon.GetComponent<NavMeshAgent>();

        slotFull = true;
        WeaponRig.weight = 1;
        Weapon.transform.SetParent(WeaponPoint);

        if (WeaponNavMeshAgent != null)
        {
            WeaponNavMeshAgent.enabled = false;
        }

        if (WeaponRigidbody != null)
        {
            Debug.Log("Weapon Rigid Body is not null");
            WeaponRigidbody.isKinematic = true;
        }

        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Weapon.transform.localScale = Vector3.one / 28.0f;
        _weaponScript = Weapon.GetComponent<WeaponScript>();
        _weaponScript.enabled = true;
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
                    if (Input.GetKey(KeyCode.E))
                    {
                        Equip(parentObject.gameObject);
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
