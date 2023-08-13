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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyAITutorial enemyAI = other.gameObject.GetComponent<EnemyAITutorial>();

            if (enemyAI != null && !enemyAI.isDead)
            {
                //Debug.Log("Thrown Enemy collapse with another Enemy in WeaponScript");
            }
        }
    }
}
