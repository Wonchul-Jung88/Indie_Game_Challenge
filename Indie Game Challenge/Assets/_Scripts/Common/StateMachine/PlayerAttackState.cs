using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState, IRootState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base( currentContext, playerStateFactory )
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.Weapon.WeaponRig.weight = 0;
        InitializeSubState();
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, true);
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        Ctx.Weapon.WeaponRig.weight = 1;
        Ctx.Animator.SetBool(Ctx.IsAttackingHash, false);
    }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsAttackPressed || Ctx.IsAttackAnimationRunning) return;
        if (Ctx.CharacterController.isGrounded) {
            SwitchState(Factory.Grounded());
        }
        else if (!Ctx.CharacterController.isGrounded)
        {
            SwitchState(Factory.Fall());
        }
    }

    public void HandleGravity()
    {
        Ctx.CurrentMovementY = Ctx.Gravity;
        Ctx.AppliedMovementY = Ctx.Gravity;
    }
}
