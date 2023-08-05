using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCController : MonoBehaviour
{
    public bool canConverse = false;
    public GameObject merchantImage;
    public GameObject converseMessage;
    public GameObject endConverseMessage;

    [SerializeField]
    private Canvas canvas;
    private GameObject _usingImage;
    private GameObject _conversationTarget;
    private PlayerStateMachine _stateMachine;

    public GameObject statsMenu;

    private void Start()
    {
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }
    }

    private void Update()
    {
        HandleImagePosition();
        HandleConversationInput();
        HandleStatsMenuInput();
    }

    private void HandleImagePosition()
    {
        if (_usingImage != null && Camera.main != null)
        {
            _usingImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 3);
        }
    }

    private void HandleConversationInput()
    {
        if (_conversationTarget != null && Input.GetKeyDown(KeyCode.T))
        {
            if (_conversationTarget.TryGetComponent<PlayerStateMachine>(out _stateMachine) && _stateMachine != null)
            {
                if (!_stateMachine.IsRunPressed)
                {
                    _stateMachine.ConversationStart();
                    ShowStatsMenu();
                    ReplaceUsingImage(endConverseMessage);
                }
            }
        }
    }

    private void HandleStatsMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) && statsMenu.activeSelf)
        {
            _stateMachine.ConversationEnd();
            HideStatsMenu();
            ReplaceUsingImage(converseMessage);
        }
    }

    public void InitiateConversation(GameObject conversationTarget)
    {
        ReplaceUsingImage(converseMessage);
        _conversationTarget = conversationTarget;
    }

    private void ReplaceUsingImage(GameObject newImage)
    {
        if (_usingImage != null)
        {
            Destroy(_usingImage);
        }

        if (newImage != null && canvas != null)
        {
            _usingImage = Instantiate(newImage, canvas.transform);
        }
    }

    public void NGConverse()
    {
        ReplaceUsingImage(merchantImage);
        _conversationTarget = null;
    }

    public void TurnHeadTowardsPlayer(Transform playerTransform)
    {
        // TODO: Implement this method
    }

    public void ResetHeadPosition()
    {
        // TODO: Implement this method
    }

    public void ShowMarker()
    {
        ReplaceUsingImage(merchantImage);
    }

    public void HideMarker()
    {
        ClearUsingImage();
    }

    private void ClearUsingImage()
    {
        if (_usingImage != null)
        {
            Destroy(_usingImage);
            _usingImage = null;
        }
    }

    public void ShowStatsMenu()
    {
        statsMenu.SetActive(true);
    }

    public void HideStatsMenu()
    {
        statsMenu.SetActive(false);
    }
}
