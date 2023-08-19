using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowState : PlayerBaseState, IExtraState
{
    public PlayerThrowState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
       : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Ctx.Weapon.IsThrowing = true;
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsThrowingHash, true);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState() { }

    public override void CheckSwitchStates() { }

    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsThrowingHash, false);
    }

    public override void InitializeSubState() { }

    public void HandleThrowAnimationEnd()
    {
        Ctx.Weapon.IsThrowing = false;
        SwitchState(Factory.Idle());
    }

    public override void InitializeExtraState() { }
}
