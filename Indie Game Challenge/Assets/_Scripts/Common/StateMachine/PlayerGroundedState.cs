using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState, IRootState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public void HandleGravity()
    {
        Ctx.CurrentMovementY = Ctx.Gravity;
        Ctx.AppliedMovementY = Ctx.Gravity;
    }

    public override void EnterState()
    {
        InitializeSubState();
        InitializeExtraState();
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates()
    {
        if (Ctx.CurrentState.SubState is PlayerTalkingState) return;

        // if player is grounded and jump is pressed, switch to jump state
        if (Ctx.InputManager.IsJumpPressed && !Ctx.InputManager.RequireNewJumpPress && !Ctx.Weapon.slotFull) {
            SwitchState(Factory.Jump());
        }
        else if (!Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Fall());
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
