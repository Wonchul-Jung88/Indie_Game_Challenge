using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
    }

    public override void UpdateState()
    {
        Ctx.StaminaController.Sprinting();
        if (Ctx.IsDash) {
            Ctx.warp.Play();
            Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.RunMultiplier * 3.0f;
            Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.RunMultiplier * 3.0f;
            CheckSwitchStates();
        }
        else {
            Ctx.warp.Stop();
            Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.RunMultiplier;
            Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.RunMultiplier;
            CheckSwitchStates();
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed || Ctx.StaminaController.playerStamina < 0)
        {
            Ctx.warp.Stop();
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsTalking)
        {
            SwitchState(Factory.Talk());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            Ctx.warp.Stop();
            SwitchState(Factory.Walk());
        }
    }
}
