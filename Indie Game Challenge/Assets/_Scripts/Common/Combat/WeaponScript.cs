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
            // NavMeshAgentの現在の速度を取得
            float speed = _navMeshAgent.velocity.magnitude;

            // 速度に基づいて回転速度を決定
            float rotationSpeed = speed * rotationFactor;

            // Y軸を中心に、毎秒 rotationSpeed度だけ時計回りに回転する
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
