using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public CharacterStatsBody _statsBody;

    //[Header("Buttons")]
    public Button SpeedBtn;
    public Button StaminaBtn;
    public Button PowerBtn;
    public Button GutsBtn;
    public Button IntelegenceBtn;

    [Header("Settings")]
    public Button selectedButton; // ���̃{�^�����I�������

    private void OnEnable()
    {
        // �{�^���Ɋ֐����A�^�b�`
        SpeedBtn.onClick.AddListener(_statsBody.AddSpeed);
        StaminaBtn.onClick.AddListener(_statsBody.AddStamina);
        PowerBtn.onClick.AddListener(_statsBody.AddPower);
        GutsBtn.onClick.AddListener(_statsBody.AddGuts);
        IntelegenceBtn.onClick.AddListener(_statsBody.AddIntelegence);

        SelectButton();
    }

    private void OnDisable()
    {
        // �{�^������֐����f�^�b�`
        SpeedBtn.onClick.RemoveListener(_statsBody.AddSpeed);
        StaminaBtn.onClick.RemoveListener(_statsBody.AddStamina);
        PowerBtn.onClick.RemoveListener(_statsBody.AddPower);
        GutsBtn.onClick.RemoveListener(_statsBody.AddGuts);
        IntelegenceBtn.onClick.RemoveListener(_statsBody.AddIntelegence);
    }

    private void SelectButton()
    {
        if (selectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // �܂��A���ݑI������Ă�����̂��N���A
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        }
    }
}
