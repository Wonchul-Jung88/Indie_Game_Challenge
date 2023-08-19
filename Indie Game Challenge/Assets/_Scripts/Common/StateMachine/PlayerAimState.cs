using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerBaseState, IExtraState
{
    public PlayerAimState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base( currentContext, playerStateFactory)
    {
        IsExtraState = true;
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsAimingHash, true);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.InputManager.IsRunPressed || Ctx.Weapon.IsThrowing)
        {
            SwitchState(Factory.DefaultExtra());
        }
    }

    public override void ExitState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsAimingHash, false);
    }

    public override void InitializeSubState() { }

    public override void InitializeExtraState() { }
}
