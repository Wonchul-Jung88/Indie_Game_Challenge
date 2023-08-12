using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLoot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine psm))
            {
                psm.GetCoin();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
