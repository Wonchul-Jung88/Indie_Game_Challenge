using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkingTrigger : MonoBehaviour
{
    public NPCController npcController;  // NPCController��Inspector����ݒ肵�܂��B

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npcController.InitiateConversation( other.gameObject );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npcController.NGConverse();
        }
    }
}
