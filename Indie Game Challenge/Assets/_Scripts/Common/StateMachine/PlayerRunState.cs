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
        Ctx.Animator.SetFloat(Ctx.AnimationManager.MoveSpeedHash, 1.0f);
    }

    public override void UpdateState()
    {
        Ctx.StaminaController.Sprinting();
        if (Ctx.IsDash) {
            Ctx.warp.Play();
            Ctx.AppliedMovementX = Ctx.InputManager.CurrentMovementInput.x * Ctx.RunMultiplier * 3.0f;
            Ctx.AppliedMovementZ = Ctx.InputManager.CurrentMovementInput.y * Ctx.RunMultiplier * 3.0f;
            CheckSwitchStates();
        }
        else {
            Ctx.warp.Stop();
            Ctx.AppliedMovementX = Ctx.InputManager.CurrentMovementInput.x * Ctx.RunMultiplier;
            Ctx.AppliedMovementZ = Ctx.InputManager.CurrentMovementInput.y * Ctx.RunMultiplier;
            CheckSwitchStates();
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.InputManager.IsMovementPressed || Ctx.StaminaController.playerStamina < 0)
        {
            Ctx.warp.Stop();
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
        else if (Ctx.InputManager.IsMovementPressed && !Ctx.InputManager.IsRunPressed)
        {
            Ctx.warp.Stop();
            SwitchState(Factory.Walk());
        }
        else if (Ctx.Weapon.slotFull && Ctx.InputManager.IsThrowPressed)
        {
            SwitchState(Factory.Throw());
        }
    }

    public override void InitializeExtraState() { }
}
