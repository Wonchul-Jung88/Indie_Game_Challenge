using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultExtraState : PlayerBaseState, IExtraState
{
    public PlayerDefaultExtraState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        IsExtraState = true;
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.Weapon.slotFull && Ctx.InputManager.IsRunPressed && !Ctx.Weapon.IsThrowing)
        {
            SwitchState(Factory.Aim());
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void InitializeExtraState() { }
}
