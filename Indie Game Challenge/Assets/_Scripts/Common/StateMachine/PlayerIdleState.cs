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
        Ctx.Animator.SetFloat(Ctx.AnimationManager.MoveSpeedHash, 0.0f);
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
        if (!Ctx.Weapon.slotFull && Ctx.InputManager.IsPickPressed && Ctx.Weapon.CanPick)
        {
            SwitchState(Factory.PickingUp());
        }
        else if (Ctx.IsTalking)
        {
            SwitchState(Factory.Talk());
        }
        else if (Ctx.InputManager.IsMovementPressed && !Ctx.InputManager.IsRunPressed)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.InputManager.IsMovementPressed && Ctx.InputManager.IsRunPressed && Ctx.StaminaController.hasRegenerated && !Ctx.Weapon.slotFull)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.Weapon.slotFull && Ctx.InputManager.IsThrowPressed) {
            SwitchState(Factory.Throw());
        }
    }

    public override void InitializeExtraState() { }
}
