using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState( PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory )
        : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.Animator.SetBool(Ctx.IsAimingHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState()
    {
        if (Ctx.IsAttackAnimationRunning) return;
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsAttackAnimationRunning) return;

        if (Ctx.IsMovementPressed && Ctx.IsRunPressed && Ctx.StaminaController.hasRegenerated)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.Weapon.slotFull && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Aim());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
}
