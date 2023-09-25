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

        // ゲームシーンに存在する「Player」タグを持つオブジェクトの一番最初のものからCharacterStatsBodyコンポーネントを取得
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
        if (_stateMachine == null) return;
        // Get the name of the current state from the PlayerStateMachine
        string stateName = _stateMachine.CurrentState?.GetType().Name ?? "null";
        string substateName = _stateMachine.CurrentState?.SubState?.GetType().Name ?? "null";
        string superstateName = _stateMachine.CurrentState?.SuperState?.GetType().Name ?? "null";
        string extrastateName = _stateMachine.CurrentState?.ExtraState?.GetType().Name ?? "null";

        // Display the name of the current state
        textMeshPro.text = "State: " + stateName + Environment.NewLine +
                           "Super State: " + superstateName + Environment.NewLine +
                           "Sub State: " + substateName + Environment.NewLine +
                           "Extra State: " + extrastateName;
    }
}
