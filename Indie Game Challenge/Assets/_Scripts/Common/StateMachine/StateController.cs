using UnityEngine;

public class StateController : MonoBehaviour
{
    // �A�j���[�V�����C�x���g����Ăяo����郁�\�b�h
    public void OnThrowAnimationEnd()
    {
        var playerStateMachine = GetComponent<PlayerStateMachine>();
        var _currentState = playerStateMachine.CurrentState;
        if (_currentState.SubState is PlayerThrowState)
        {
            (_currentState.SubState as PlayerThrowState).HandleThrowAnimationEnd();
        }
    }

    public void OnPickingUpAnimationEnd()
    {
        var playerStateMachine = GetComponent<PlayerStateMachine>();
        var _currentState = playerStateMachine.CurrentState;
        if (_currentState.SubState is PlayerPickingUpState)
        {
            Debug.Log("Picking Animation End ~~ ");
            (_currentState.SubState as PlayerPickingUpState).HandlePickingUpAnimationEnd();
        }
    }
}
