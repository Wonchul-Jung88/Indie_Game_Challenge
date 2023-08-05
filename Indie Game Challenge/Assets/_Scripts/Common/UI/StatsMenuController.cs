using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    //[Header("Buttons")]
    //public Button button1;
    //public Button button2;
    //public Button button3;
    //public Button button4;
    //public Button button5;

    [Header("Settings")]
    public Button selectedButton; // このボタンが選択される

    private void OnEnable()
    {
        SelectButton();
    }

    private void SelectButton()
    {
        if (selectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // まず、現在選択されているものをクリア
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        }
    }
}
