using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickingUpState : PlayerBaseState
{
    public PlayerPickingUpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
       : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsPickingUpHash, true);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState() { }

    public override void CheckSwitchStates() { }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public void HandlePickingUpAnimationEnd()
    {
        SwitchState(Factory.Idle());
    }
}
