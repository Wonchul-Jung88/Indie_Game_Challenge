using UnityEngine;
using TMPro;
using System;

public class DisplayPlayerState : MonoBehaviour
{
    public PlayerStateMachine playerStateMachine; // The PlayerStateMachine component

    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
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
