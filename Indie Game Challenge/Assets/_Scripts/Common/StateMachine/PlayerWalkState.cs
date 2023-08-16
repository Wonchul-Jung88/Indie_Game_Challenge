using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base( currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsRunningHash, false);
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsAimingHash, false);
    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.InputManager.CurrentMovementInput.x * Ctx.WalkMultiplier * Ctx.StaminaController.exhaustedFactor;
        Ctx.AppliedMovementZ = Ctx.InputManager.CurrentMovementInput.y * Ctx.WalkMultiplier * Ctx.StaminaController.exhaustedFactor;
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.InputManager.IsMovementPressed || (Ctx.InputManager.IsRunPressed && !Ctx.StaminaController.hasRegenerated))
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsTalking)
        {
            SwitchState(Factory.Talk());
        }
        else if (!Ctx.Weapon.slotFull && Ctx.InputManager.IsPickPressed && Ctx.Weapon.CanPick)
        {
            SwitchState(Factory.PickingUp());
        }
        else if (Ctx.Weapon.slotFull && Ctx.InputManager.IsRunPressed) {
            SwitchState(Factory.Aim());
        }
        else if (Ctx.InputManager.IsMovementPressed && Ctx.InputManager.IsRunPressed && Ctx.StaminaController.hasRegenerated)
        {
            SwitchState(Factory.Run());
        }
    }
}
