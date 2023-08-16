using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerBaseState
{
    public PlayerAimState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base( currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsAimingHash, true);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.InputManager.IsThrowPressed)
        {
            SwitchState(Factory.Throw());
        }
        else if ( !Ctx.InputManager.IsRunPressed && !Ctx.InputManager.IsMovementPressed ) {
            SwitchState(Factory.Idle());
        }
        else if ( !Ctx.InputManager.IsRunPressed && Ctx.InputManager.IsMovementPressed )
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }
}
