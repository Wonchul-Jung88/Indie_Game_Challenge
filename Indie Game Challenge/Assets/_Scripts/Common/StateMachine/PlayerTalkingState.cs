using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalkingState : PlayerBaseState
{
    public PlayerTalkingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        Ctx.Animator.SetBool(Ctx.AnimationManager.IsTalkingHash, true);
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
        if (!Ctx.IsTalking)
        {
            SwitchState(Factory.Idle());
        }
    }
}
