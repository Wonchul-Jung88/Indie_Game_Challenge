using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRecognitionTrigger : MonoBehaviour
{
    public NPCController npcController;  // NPCControllerをInspectorから設定します。

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npcController.TurnHeadTowardsPlayer(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npcController.ResetHeadPosition();
        }
    }
}
