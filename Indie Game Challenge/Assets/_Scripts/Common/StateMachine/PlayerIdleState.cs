using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState( PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsRunningHash, false);
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsAimingHash, false);
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsThrowingHash, false);
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsPickingUpHash, false);
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsTalkingHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (Ctx.InputManager.IsMovementPressed && Ctx.InputManager.IsRunPressed && Ctx.StaminaController.hasRegenerated)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.IsTalking)
        {
            SwitchState(Factory.Talk());
        }
        else if (!Ctx.Weapon.slotFull && Ctx.InputManager.IsPickPressed && Ctx.Weapon.CanPick)
        {
            SwitchState(Factory.PickingUp());
        }

        else if (Ctx.Weapon.slotFull && Ctx.InputManager.IsRunPressed)
        {
            SwitchState(Factory.Aim());
        }
        else if (Ctx.InputManager.IsMovementPressed && !Ctx.InputManager.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
}
