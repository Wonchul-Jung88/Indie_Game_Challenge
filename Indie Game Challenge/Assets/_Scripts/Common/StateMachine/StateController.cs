using UnityEngine;

public class StateController : MonoBehaviour
{
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
            (_currentState.SubState as PlayerPickingUpState).HandlePickingUpAnimationEnd();
        }
    }
}
