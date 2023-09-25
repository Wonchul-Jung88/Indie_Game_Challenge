using UnityEngine;
using TMPro;

public class DisplayCharacterStats : MonoBehaviour
{
    private CharacterStatsBody _statsBody;

    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            _statsBody = player.GetComponent<CharacterStatsBody>();
            if (_statsBody == null)
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
        if (_statsBody == null) return;

        var speedValue = _statsBody.Speed.Value;
        var staminaValue = _statsBody.Stamina.Value;
        var powerValue = _statsBody.Power.Value;
        var gutsValue = _statsBody.Guts.Value;
        var intelligenceValue = _statsBody.Intelligence.Value;  // スペルミスを修正

        textMeshPro.text = $"Speed = {speedValue}\n" +
                           $"Stamina = {staminaValue}\n" +
                           $"Power = {powerValue}\n" +
                           $"Guts = {gutsValue}\n" +
                           $"Intelligence = {intelligenceValue}";  // スペルミスを修正
    }
}
