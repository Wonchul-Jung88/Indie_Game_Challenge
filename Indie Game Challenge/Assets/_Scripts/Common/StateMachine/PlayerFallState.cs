using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState, IRootState
{
    public PlayerFallState( PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base( currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        InitializeExtraState();
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsFallingHash, true);
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsFallingHash, false);
    }

    public void HandleGravity() {
        float previousYVelocity = Ctx.CurrentMovementY;
        Ctx.CurrentMovementY = Ctx.CurrentMovementY + Ctx.Gravity * Time.deltaTime;
        Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * .5f, -20.0f);
    }

    public override void CheckSwitchStates()
    {
        // if player is grounded, switch to the grounded state
        if (Ctx.GroundCheck.IsGrounded) {
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        if (!Ctx.InputManager.IsMovementPressed && !Ctx.InputManager.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.InputManager.IsMovementPressed && !Ctx.InputManager.IsRunPressed)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }

    public override void InitializeExtraState()
    {
        SetExtraState(Factory.DefaultExtra());
    }
}
