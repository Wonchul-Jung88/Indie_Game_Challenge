using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Update the text to display the current number of coins
        textMeshPro.text = "Coins: " + CoinManager.Instance.CoinsCollected;
    }
}
