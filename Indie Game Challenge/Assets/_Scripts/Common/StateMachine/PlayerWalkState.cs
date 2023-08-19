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
        Ctx.Animator.SetFloat(Ctx.AnimationManager.MoveSpeedHash, 0.5f);
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
        else if (!Ctx.Weapon.slotFull && Ctx.InputManager.IsPickPressed && Ctx.Weapon.CanPick)
        {
            SwitchState(Factory.PickingUp());
        }
        else if (Ctx.IsTalking)
        {
            SwitchState(Factory.Talk());
        }
        else if (Ctx.InputManager.IsMovementPressed && Ctx.InputManager.IsRunPressed && Ctx.StaminaController.hasRegenerated && !Ctx.Weapon.slotFull)
        {
            SwitchState(Factory.Run());
        }
        else if (Ctx.Weapon.slotFull && Ctx.InputManager.IsThrowPressed)
        {
            SwitchState(Factory.Throw());
        }
    }

    public override void InitializeExtraState() { }
}
