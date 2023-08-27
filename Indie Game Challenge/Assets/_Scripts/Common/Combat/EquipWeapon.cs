using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class EquipWeapon : MonoBehaviour
{
    GameObject Weapon;
    PlayerInputManager _inputManager;
    GameObject _targetObject;

    public Transform WeaponPoint, Player;
    public Rig WeaponRig;
    public float throwPower = 100;

    public bool slotFull;
    public bool CanPick => _targetObject != null;
    public bool IsThrowing = false;

    public void AwakeInitialize( PlayerInputManager inputManager )
    {
        _inputManager = inputManager;
    }

    public void ThrowWeapon()
    {
        ReleaseVariables();

        
        Weapon.GetComponent<WeaponScript>().activated = true;
        var _weaponRigidbody = Weapon.GetComponent<Rigidbody>();
        _weaponRigidbody.isKinematic = false;
        _weaponRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Weapon.transform.SetParent(null);
        Weapon.transform.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
        Weapon.transform.position += Player.right / 5;
        Vector3 forceToAdd = Camera.main.transform.forward * throwPower + transform.up * 2;//Player.forward * throwPower;// + Player.up * 2f;
        _weaponRigidbody.AddForce(forceToAdd, ForceMode.Impulse);
        Weapon.GetComponent<Collider>().enabled = true;
        Weapon.GetComponent<Collider>().isTrigger = false;
    }

    public void WeaponThrow()
    {
        //Aim(false, true, 1f);

        //hasWeapon = false;
        //weaponScript.activated = true;
        //weaponRb.isKinematic = false;
        //weaponRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        //weapon.parent = null;
        //weapon.eulerAngles = new Vector3(0, -90 + transform.eulerAngles.y, 0);
        //weapon.transform.position += transform.right / 5;
        //weaponRb.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode.Impulse);

        //Trail
        //trailRenderer.emitting = true;
        //trailParticle.Play();
    }

    void Equip()
    {
        if (_targetObject == null) return;
        Weapon = _targetObject;

        var _weaponRigidbody = Weapon.GetComponent<Rigidbody>();

        Weapon.transform.SetParent(WeaponPoint);

        if (_weaponRigidbody != null)
        {
            _weaponRigidbody.isKinematic = true;
        }

        //Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.position = WeaponPoint.position;
        //Weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Weapon.transform.eulerAngles = WeaponPoint.eulerAngles;
        //Weapon.transform.localScale = Vector3.one / 28.0f;

        PostEquip();
    }


    private void PostEquip()
    {
        _targetObject = null;
        WeaponRig.weight = 1;
        slotFull = true;
    }

    private void ReleaseVariables()
    {
        _targetObject = null;
        WeaponRig.weight = 0;
        slotFull = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool hasEnemyAI = other.TryGetComponent<EnemyAITutorial>(out EnemyAITutorial enemyAI);

        
        if (hasEnemyAI && enemyAI != null && enemyAI.isDead && !slotFull && _targetObject == null)
        {
            _targetObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool hasEnemyAI = other.TryGetComponent<EnemyAITutorial>(out EnemyAITutorial enemyAI);

        if (other.gameObject == _targetObject)
        {
            _targetObject = null;
        }
    }
}
