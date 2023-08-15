using UnityEngine;
using TMPro;
using System;

public class DisplayPlayerState : MonoBehaviour
{
    private PlayerStateMachine playerStateMachine; // The PlayerStateMachine component

    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // ゲームシーンに存在する「Player」タグを持つオブジェクトの一番最初のものからCharacterStatsBodyコンポーネントを取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerStateMachine = player.GetComponent<PlayerStateMachine>();
            if (playerStateMachine == null)
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
        string stateName = playerStateMachine.CurrentState?.GetType().Name ?? "null";
        string substateName = playerStateMachine.CurrentState?.SubState?.GetType().Name ?? "null";
        string superstateName = playerStateMachine.CurrentState?.SuperState?.GetType().Name ?? "null";

        // Display the name of the current state
        textMeshPro.text = "State: " + stateName + Environment.NewLine +
                           "Super State: " + superstateName + Environment.NewLine +
                           "Sub State: " + substateName;
    }
}
