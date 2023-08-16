using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerBaseState _currentState;  // ���݂̃v���C���[�̏��

    private PlayerStateMachine playerStateMachine;

    private void Start()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
        _currentState = playerStateMachine.CurrentState;
    }

    // �A�j���[�V�����C�x���g����Ăяo����郁�\�b�h
    public void OnThrowAnimationEnd()
    {
        if (_currentState.SubState is PlayerThrowState)
        {
            (_currentState.SubState as PlayerThrowState).HandleThrowAnimationEnd();
        }
    }

    public void OnPickingUpAnimationEnd()
    {
        if (_currentState.SubState is PlayerPickingUpState)
        {
            (_currentState.SubState as PlayerPickingUpState).HandlePickingUpAnimationEnd();
        }
    }
}
