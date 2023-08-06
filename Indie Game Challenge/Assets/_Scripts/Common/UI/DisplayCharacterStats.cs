using UnityEngine;
using TMPro;

public class DisplayCharacterStats : MonoBehaviour
{
    public CharacterStatsBody _statsBody; // The PlayerStateMachine component

    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var speedValue = _statsBody.Speed.Value;
        var staminaValue = _statsBody.Stamina.Value;
        var powerValue = _statsBody.Power.Value;
        var gutsValue = _statsBody.Guts.Value;
        var intelegenceValue = _statsBody.Intelegence.Value;

        // Set the text of TextMeshProUGUI
        textMeshPro.text = $"Speed = {speedValue}\n" +
                           $"Stamina = {staminaValue}\n" +
                           $"Power = {powerValue}\n" +
                           $"Guts = {gutsValue}\n" +
                           $"Intelegence = {intelegenceValue}";
    }
}
