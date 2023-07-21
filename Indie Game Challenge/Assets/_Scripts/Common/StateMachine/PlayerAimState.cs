using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerBaseState
{
    public PlayerAimState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory )
        : base( currentContext, playerStateFactory )
    {
    }

    public override void EnterState()
    {
        Ctx.Weapon.WeaponRig.weight = 0;
        Ctx.Animator.SetBool(Ctx.IsAimingHash, true);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetKey(KeyCode.F)) {
            Ctx.Animator.SetTrigger("Throw");
            SwitchState(Factory.Idle());
        }
        else if ( !Ctx.IsRunPressed && !Ctx.IsMovementPressed ) {
            Ctx.Weapon.WeaponRig.weight = 1;
            SwitchState(Factory.Idle());
        }
        else if ( !Ctx.IsRunPressed && Ctx.IsMovementPressed )
        {
            Ctx.Weapon.WeaponRig.weight = 1;
            SwitchState(Factory.Walk());
        }
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    
}
