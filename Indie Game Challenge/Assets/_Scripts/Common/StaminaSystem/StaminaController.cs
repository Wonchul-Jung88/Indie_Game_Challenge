using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=Fs2YCoamO_U
public class StaminaController : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float jumpCost = 20;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool weAreSprinting = false;
    public float exhaustedFactor = 1.0f;

    [Header("Stamina Regen Parameters")]
    [Range(0, 50)][SerializeField] private float staminaDrain = 10f;
    [Range(0, 50)][SerializeField] private float staminaRegen = 10f;

    //[Header("Stamina Speed Parameters")]
    //[SerializeField] private int slowedRunSpeed = 4;
    //[SerializeField] private int normalRunSpeed = 8;

    [Header("Stamina UI Elements")]
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

    //private PlayerStateMachine player;
    private PlayerInputManager _inputManager;

    private void Start()
    {
        //player = GetComponent<PlayerStateMachine>();
        _inputManager = PlayerInputManager.Instance;
    }

    private void Update()
    {
        if ( !_inputManager.IsRunPressed ) {
            if ( playerStamina <= maxStamina - 0.01 ) {
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStamina(1);

                if ( playerStamina >= maxStamina )
                {
                    sliderCanvasGroup.alpha = 0;
                    exhaustedFactor = 1.0f;
                    hasRegenerated = true;
                }
            }
        }
    }

    public void Sprinting()
    {
        if (hasRegenerated)
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina <= 0)
            {
                hasRegenerated = false;
                //slow the player
                sliderCanvasGroup.alpha = 0;
                exhaustedFactor = 0.1f;
            }
        }
    }

    public void StaminaJump()
    {
        if (_inputManager.IsJumpPressed && playerStamina >= (maxStamina * jumpCost / maxStamina))
        {
            playerStamina -= jumpCost;
            UpdateStamina(1);
        }
    }

    void UpdateStamina(int value)
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;
        sliderCanvasGroup.alpha = value;
    }
}
