using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base( currentContext, playerStateFactory )
    {
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.Animator.SetBool(Ctx.IsAimingHash, false);
    }

    public override void UpdateState()
    {
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.WalkMultiplier * Ctx.StaminaController.exhaustedFactor;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.WalkMultiplier * Ctx.StaminaController.exhaustedFactor;
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed || (Ctx.IsRunPressed && !Ctx.StaminaController.hasRegenerated))
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsTalking)
        {
            SwitchState(Factory.Talk());
        }
        else if (Ctx.Weapon.slotFull && Ctx.IsRunPressed) {
            SwitchState(Factory.Aim());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsRunPressed && Ctx.StaminaController.hasRegenerated)
        {
            SwitchState(Factory.Run());
        }
    }
}
