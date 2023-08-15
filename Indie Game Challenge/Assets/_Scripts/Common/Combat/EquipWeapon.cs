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
        var _weaponRigidbody = Weapon.GetComponent<Rigidbody>();

        Weapon.GetComponent<Collider>().enabled = true;
        Weapon.GetComponent<Collider>().isTrigger = false;
        WeaponRig.weight = 0;

        Weapon.GetComponent<WeaponScript>().activated = true;
        _weaponRigidbody.isKinematic = false;
        _weaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

        Weapon.transform.parent = null;
        //Weapon.transform.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
        Weapon.transform.position += transform.right / 5;
        //_weaponRigidbody.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode.Impulse);
        _weaponRigidbody.AddForce(transform.forward * throwPower + transform.up * 2f, ForceMode.Impulse);
        slotFull = false;
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
        _weaponRigidbody.AddForce(Camera.main.transform.forward * throwPower + transform.up * 5, ForceMode.Impulse);
    }

    void Equip( GameObject _weapon )
    {
        Weapon = _weapon;
        if (Weapon == null) return;
        var _weaponRigidbody = Weapon.GetComponent<Rigidbody>();

        slotFull = true;
        WeaponRig.weight = 1;
        Weapon.transform.SetParent(WeaponPoint);

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
        if (other.TryGetComponent<EnemyAITutorial>(out EnemyAITutorial enemyAI)
            && enemyAI.isDead
            && Input.GetKey(KeyCode.E)
            && !slotFull)
        {
            Equip(other.gameObject);
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
