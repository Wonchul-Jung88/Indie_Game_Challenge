using UnityEngine;

public class StateController : MonoBehaviour
{
    // アニメーションイベントから呼び出されるメソッド
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
