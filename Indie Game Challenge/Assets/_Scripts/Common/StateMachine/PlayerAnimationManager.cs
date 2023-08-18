using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    int _isWalkingHash;
    int _isRunningHash;
    int _isFallingHash;
    int _isJumpingHash;
    int _jumpCountHash;
    int _isAttackingHash;
    int _isAimingHash;
    int _isPickingUpHash;
    int _isTalkingHash;
    int _isThrowingHash;
    int _throwHash;

    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int IsJumpingHash { get { return _isJumpingHash; } }
    public int IsFallingHash { get { return _isFallingHash; } }
    public int IsAttackingHash { get { return _isAttackingHash; } }
    public int IsAimingHash { get { return _isAimingHash; } }
    public int IsPickingUpHash { get { return _isPickingUpHash; } }
    public int IsTalkingHash { get { return _isTalkingHash; } }
    public int IsThrowingHash { get { return _isThrowingHash; } }
    public int ThrowHash { get { return _throwHash; } }
    public int JumpCountHash { get { return _jumpCountHash; } }
    public static PlayerAnimationManager Instance;
    private void Awake()
    {
        _isWalkingHash = Animator.StringToHash("IsWalking");
        _isRunningHash = Animator.StringToHash("IsRunning");
        _isJumpingHash = Animator.StringToHash("IsJumping");
        _jumpCountHash = Animator.StringToHash("JumpCount");
        _isFallingHash = Animator.StringToHash("IsFalling");
        _isAttackingHash = Animator.StringToHash("IsAttacking");
        _isAimingHash = Animator.StringToHash("IsAiming");
        _isTalkingHash = Animator.StringToHash("IsTalking");
        _isPickingUpHash = Animator.StringToHash("IsPickingUp");
        _isThrowingHash = Animator.StringToHash("IsThrowing");
        _throwHash = Animator.StringToHash("Throw");

        Instance = this;
    }

    public void AwakeInitialize()
    {
        _isWalkingHash = Animator.StringToHash("IsWalking");
        _isRunningHash = Animator.StringToHash("IsRunning");
        _isJumpingHash = Animator.StringToHash("IsJumping");
        _jumpCountHash = Animator.StringToHash("JumpCount");
        _isFallingHash = Animator.StringToHash("IsFalling");
        _isAttackingHash = Animator.StringToHash("IsAttacking");
        _isAimingHash = Animator.StringToHash("IsAiming");
        _isTalkingHash = Animator.StringToHash("IsTalking");
        _isPickingUpHash = Animator.StringToHash("IsPickingUp");
        _isThrowingHash = Animator.StringToHash("IsThrowing");
        _throwHash = Animator.StringToHash("Throw");
    }
}
