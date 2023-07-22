using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class EquipWeapon : MonoBehaviour
{
    GameObject Weapon;

    public Transform WeaponPoint, Player;
    public Rig WeaponRig;
    public float throwPower = 100;

    public bool slotFull;

    public void ThrowWeapon()
    {
        slotFull = false;

        var _weaponRigidbody = Weapon.GetComponent<Rigidbody>();
        var _weaponNavMeshAgent = Weapon.GetComponent<NavMeshAgent>();

        _weaponRigidbody.isKinematic = true;
        _weaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        if (_weaponNavMeshAgent != null) _weaponNavMeshAgent.enabled = true;

        Weapon.transform.parent = null;
        //Weapon.transform.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
        Weapon.transform.position += transform.right / 5;
        _weaponNavMeshAgent.velocity = transform.forward * throwPower;

        WeaponRig.weight = 0;
    }

    public void ThrowDynamite()
    {
        slotFull = false;

        var _weaponRigidbody = Weapon.GetComponent<Rigidbody>();
        _weaponRigidbody.isKinematic = false;
        _weaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        //_tempWeapon.transform.parent = null;
        //_tempWeapon.transform.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
        //_tempWeapon.transform.position += transform.right / 5;
        _weaponRigidbody.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode.Impulse);
    }

    void Equip( GameObject _weapon )
    {
        Weapon = _weapon;
        if (Weapon == null) return;
        var _weaponRigidbody = Weapon.GetComponent<Rigidbody>();
        var _weaponNavMeshAgent = Weapon.GetComponent<NavMeshAgent>();

        slotFull = true;
        WeaponRig.weight = 1;
        Weapon.transform.SetParent(WeaponPoint);

        if (_weaponNavMeshAgent != null)
        {
            _weaponNavMeshAgent.enabled = false;
        }

        if (_weaponRigidbody != null)
        {
            _weaponRigidbody.isKinematic = true;
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
