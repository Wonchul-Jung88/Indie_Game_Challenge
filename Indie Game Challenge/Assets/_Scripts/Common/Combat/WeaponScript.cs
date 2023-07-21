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
            //transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enabled) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("WhatIsGround"))
        {
            Debug.Log("Touch Ground Weapon Script");
            GetComponent<NavMeshAgent>().enabled = true;
            print(collision.gameObject.name);
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            activated = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Breakable"))
    //    {
    //        if (other.GetComponent<BreakBoxScript>() != null)
    //        {
    //            other.GetComponent<BreakBoxScript>().Break();
    //        }
    //    }
    //}
}
