using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCController : MonoBehaviour
{
    public bool canConverse = false;
    public GameObject _merchantImage;
    public GameObject _ConverseMessage;

    private GameObject _usingImage;

    private void Update()
    {
        if (_usingImage != null)
        {
            _usingImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + 3 * Vector3.up);
        }
    }

    private void ReplaceUsingImage(GameObject newImage)
    {
        // Destroy the current _usingImage if it exists
        if (_usingImage != null)
        {
            Destroy(_usingImage);
        }

        // Instantiate the new image
        Canvas canvas = FindObjectOfType<Canvas>();
        if (newImage != null && canvas != null)
        {
            _usingImage = Instantiate(newImage, canvas.transform);
        }
    }

    private void ClearUsingImage()
    {
        if (_usingImage != null)
        {
            Destroy(_usingImage);
            _usingImage = null;
        }
    }


    public void OKConverse()
    {
        ReplaceUsingImage(_ConverseMessage);
    }

    public void NGConverse()
    {
        ReplaceUsingImage(_merchantImage);
    }

    public void TurnHeadTowardsPlayer(Transform playerTransform)
    {
    }

    public void ResetHeadPosition()
    {

    }

    // Method to show the marker
    public void ShowMarker()
    {
        ReplaceUsingImage(_merchantImage);
    }

    // Method to hide the marker
    public void HideMarker()
    {
        // Instead of destroying _usingImage here, we simply set it to null
        // as the ReplaceUsingImage method now handles the destruction
        ClearUsingImage();
    }
}
