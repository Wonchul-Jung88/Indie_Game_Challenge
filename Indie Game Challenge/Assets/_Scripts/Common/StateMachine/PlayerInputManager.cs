using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerInput _playerInput;
    bool _isMovementPressed = false;
    bool _isRunPressed = false;
    bool _isJumpPressed = false;
    bool _isAttackPressed = false;
    bool _isPickPressed = false;
    bool _isTalkPressed = false;
    bool _isThrowPressed = false;
    bool _isStatsMenuOpenPressed = false;
    Vector2 _currentMovementInput = Vector2.zero;
    bool _requireNewJumpPress = false;

    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsAttackPressed { get { return _isAttackPressed; } }
    public bool IsPickPressed { get { return _isPickPressed; } }
    public bool IsStatsMenuOpenPressed { get { return _isStatsMenuOpenPressed; } }
    public bool IsTalkPressed { get { return _isTalkPressed; } }
    public bool IsThrowPressed { get { return _isThrowPressed; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public PlayerInput PlayerInput { get { return _playerInput; } }

    public static PlayerInputManager Instance;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        // set the player input callbacks
        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Run.started += onRun;
        _playerInput.CharacterControls.Run.canceled += onRun;
        _playerInput.CharacterControls.Jump.started += onJump;
        _playerInput.CharacterControls.Jump.canceled += onJump;
        _playerInput.CharacterControls.Attack.started += onAttack;
        _playerInput.CharacterControls.Attack.canceled += onAttack;
        _playerInput.CharacterControls.Talk.started += onTalk;
        _playerInput.CharacterControls.Talk.canceled += onTalk;
        _playerInput.CharacterControls.Throw.started += onThrow;
        _playerInput.CharacterControls.Throw.canceled += onThrow;
        _playerInput.CharacterControls.Pick.started += onPick;
        _playerInput.CharacterControls.Pick.canceled += onPick;
        _playerInput.CharacterControls.StatsMenuOpen.started += onStatsMenuOpen;
        _playerInput.CharacterControls.StatsMenuOpen.canceled += onStatsMenuOpen;

        Instance = this;
    }

    public void AwakeInitialize()
    {
        _playerInput = new PlayerInput();

        // set the player input callbacks
        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Run.started += onRun;
        _playerInput.CharacterControls.Run.canceled += onRun;
        _playerInput.CharacterControls.Jump.started += onJump;
        _playerInput.CharacterControls.Jump.canceled += onJump;
        _playerInput.CharacterControls.Attack.started += onAttack;
        _playerInput.CharacterControls.Attack.canceled += onAttack;
        _playerInput.CharacterControls.Talk.started += onTalk;
        _playerInput.CharacterControls.Talk.canceled += onTalk;
        _playerInput.CharacterControls.Throw.started += onThrow;
        _playerInput.CharacterControls.Throw.canceled += onThrow;
        _playerInput.CharacterControls.Pick.started += onPick;
        _playerInput.CharacterControls.Pick.canceled += onPick;
        _playerInput.CharacterControls.StatsMenuOpen.started += onStatsMenuOpen;
        _playerInput.CharacterControls.StatsMenuOpen.canceled += onStatsMenuOpen;
    }

    private void onRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    private void onJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _requireNewJumpPress = false;
    }

    private void onAttack(InputAction.CallbackContext context)
    {
        _isAttackPressed = context.ReadValueAsButton();
    }

    private void onTalk(InputAction.CallbackContext context)
    {
        _isTalkPressed = context.ReadValueAsButton();
    }

    private void onPick(InputAction.CallbackContext context)
    {
        _isPickPressed = context.ReadValueAsButton();
    }

    private void onStatsMenuOpen(InputAction.CallbackContext context)
    {
        _isStatsMenuOpenPressed = context.ReadValueAsButton();
    }

    private void onThrow(InputAction.CallbackContext context)
    {
        _isThrowPressed = context.ReadValueAsButton();
    }

    private void onMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0.0f || _currentMovementInput.y != 0.0f;
    }
}
