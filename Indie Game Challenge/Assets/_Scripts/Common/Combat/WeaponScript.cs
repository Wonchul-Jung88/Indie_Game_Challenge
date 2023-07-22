using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeaponScript : MonoBehaviour
{

    public bool activated;

    public float rotationFactor = 30;

    void Update()
    {
        if (activated)
        {
            var _navMeshAgent = GetComponent<NavMeshAgent>();
            // NavMeshAgent�̌��݂̑��x���擾
            float speed = _navMeshAgent.velocity.magnitude;

            // ���x�Ɋ�Â��ĉ�]���x������
            float rotationSpeed = speed * rotationFactor;

            // Y���𒆐S�ɁA���b rotationSpeed�x�������v���ɉ�]����
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!enabled) return;
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("WhatIsGround"))
    //    {
    //        Debug.Log("Touch Ground Weapon Script");
    //        GetComponent<NavMeshAgent>().enabled = true;
    //        print(collision.gameObject.name);
    //        GetComponent<Rigidbody>().Sleep();
    //        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    //        GetComponent<Rigidbody>().isKinematic = true;
    //        activated = false;
    //    }
    //}
}
