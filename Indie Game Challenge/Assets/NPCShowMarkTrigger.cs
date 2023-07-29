using UnityEngine;

public class NPCShowMarkTrigger : MonoBehaviour
{
    private NPCController npcController;

    private void Start()
    {
        npcController = GetComponentInParent<NPCController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            npcController.ShowMarker();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            npcController.HideMarker();
        }
    }
}
