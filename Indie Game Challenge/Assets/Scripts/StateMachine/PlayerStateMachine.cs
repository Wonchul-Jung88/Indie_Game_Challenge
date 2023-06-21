using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // declare reference variables
    CharacterController _characterController;
    Animator _animator;
    PlayerInput _playerInput; // NOTE : PlayerInput class must be generated from New Input System in Inspector

    // variables to store optimized setter/getter parameter IDs
    int _isWalkingHash;
    int _isRunningHash;
    int _isFallingHash;

    // variables to store player input values
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _appliedMovement;
    Vector3 _cameraRelativeMovement;
    bool _isMovementPressed;
    bool _isRunPressed;

    // constants
    float _rotationFactorPerFrame = 15.0f;
    float _runMultiplier = 4.0f;
    float _zero = 0.0f;

    // gravity variables
    float _gravity = -9.8f;

    // jumping variables
    bool _isJumpPressed = false;
    float _initialJumpVelocity;
    float _maxJumpHeight = 4.0f;
    float _maxJumpTime = .75f;
    bool _isJumping = false;
    int _isJumpingHash;
    int _jumpCountHash;
    bool _requireNewJumpPress = false;
    int _jumpCount = 0;
    Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
    Coroutine _currentJumpResetRoutine = null;

    // state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _animator; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public Coroutine CurrentJumpResetRoutine { get { return _currentJumpResetRoutine; } set { _currentJumpResetRoutine = value; } }
    public Dictionary<int, float> InitialJumpVelocities { get { return _initialJumpVelocities; } }
    public Dictionary<int, float> JumpGravities { get { return _jumpGravities; } }
    public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int IsJumpingHash { get { return _isJumpingHash; } }
    public int IsFallingHash { get { return _isFallingHash; } }
    public int JumpCountHash { get { return _jumpCountHash; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; }  set { _requireNewJumpPress = value; } }
    public bool IsJumping { set { _isJumping = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public float Gravity { get { return _gravity; } }
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }
    public float RunMultiplier { get { return _runMultiplier; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }

    private void Awake()
    {
        // initially set reference variables
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        // set the parameter hash references
        _isWalkingHash = Animator.StringToHash("IsWalking");
        _isRunningHash = Animator.StringToHash("IsRunning");
        //_isJumpingHash = Animator.StringToHash("IsJumping");
        //_jumpCountHash = Animator.StringToHash("JumpCount");
        //_isFallingHash = Animator.StringToHash("IsFalling");

        // set the player input callbacks
        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Run.started += onRun;
        _playerInput.CharacterControls.Run.canceled += onRun;
        //_playerInput.CharacterControls.jump.started += onJump;
        //_playerInput.CharacterControls.jump.canceled += onJump;
    }

    private void Start()
    {
        _characterController.Move(_appliedMovement * Time.deltaTime);
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
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

    private void onMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != _zero || _currentMovementInput.y != _zero;
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;
        // the change in position our character should point to
        positionToLookAt.x = _cameraRelativeMovement.x;
        positionToLookAt.y = _zero;
        positionToLookAt.z = _cameraRelativeMovement.z;
        // the current rotation of our character
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            // creates a new rotation based on where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            // rotate the character to face the positionToLookAt
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void SetJumpVariables()
    {
        float timeToApex = _maxJumpHeight / 2;
        float initialGravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVeocity = (2 * (_maxJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight + 4)) / (timeToApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondJumpInitialVeocity);
        _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        _jumpGravities.Add(0, initialGravity);
        _jumpGravities.Add(1, initialGravity);
        _jumpGravities.Add(2, secondJumpGravity);
        _jumpGravities.Add(3, thirdJumpGravity);
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        _currentState.UpdateStates();

        _cameraRelativeMovement = ConvertToCameraSpace(_appliedMovement);
        _characterController.Move(_cameraRelativeMovement * Time.deltaTime);
    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        // store the Y Value of the original vector to rotate
        float currentYValue = vectorToRotate.y;

        // get the forward and right directional vectors of the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // remove the Y values to ignore upward/downward camera angles
        cameraForward.y = 0;
        cameraRight.y = 0;

        // re-normalize both vectors so they each have a magnitude of 1
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        // rotate the X and Z VectorToRotate values to camera space
        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        // the sum of both products is the Vector3 in camera space
        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }
}
