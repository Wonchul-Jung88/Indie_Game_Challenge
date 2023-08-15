using UnityEngine;
using TMPro;

public class DisplayCharacterStats : MonoBehaviour
{
    private CharacterStatsBody _statsBody; // The PlayerStateMachine component

    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // ゲームシーンに存在する「Player」タグを持つオブジェクトの一番最初のものからCharacterStatsBodyコンポーネントを取得
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
