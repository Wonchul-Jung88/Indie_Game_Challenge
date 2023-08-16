using UnityEngine;
using TMPro;
using System;

public class DisplayPlayerState : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // �Q�[���V�[���ɑ��݂���uPlayer�v�^�O�����I�u�W�F�N�g�̈�ԍŏ��̂��̂���CharacterStatsBody�R���|�[�l���g���擾
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            _stateMachine = player.GetComponent<PlayerStateMachine>();
            if (_stateMachine == null)
            {
                Debug.LogWarning("CharacterStatsBody component not found on player object.");
            }
        }
        else
        {
            Debug.LogWarning("Player object with 'Player' tag not found in the scene.");
        }
    }


    void Update()
    {
        // Get the name of the current state from the PlayerStateMachine
        string stateName = _stateMachine.CurrentState?.GetType().Name ?? "null";
        string substateName = _stateMachine.CurrentState?.SubState?.GetType().Name ?? "null";
        string superstateName = _stateMachine.CurrentState?.SuperState?.GetType().Name ?? "null";

        // Display the name of the current state
        textMeshPro.text = "State: " + stateName + Environment.NewLine +
                           "Super State: " + superstateName + Environment.NewLine +
                           "Sub State: " + substateName;
    }
}
