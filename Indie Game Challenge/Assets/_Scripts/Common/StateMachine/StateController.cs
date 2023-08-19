using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerBaseState _currentState;  // 現在のプレイヤーの状態

    private PlayerStateMachine playerStateMachine;

    //private void Start()
    //{
    //    playerStateMachine = GetComponent<PlayerStateMachine>();
    //    _currentState = playerStateMachine.CurrentState;
    //}

    private bool hasInitialized = false;

    private void Update()
    {
        if (!hasInitialized)
        {
            playerStateMachine = GetComponent<PlayerStateMachine>();
            _currentState = playerStateMachine.CurrentState;
            hasInitialized = true;
        }
    }


    // アニメーションイベントから呼び出されるメソッド
    public void OnThrowAnimationEnd()
    {
        if (_currentState.SubState is PlayerThrowState)
        {
            (_currentState.SubState as PlayerThrowState).HandleThrowAnimationEnd();
        }
    }

    public void OnThrowAnimationStart()
    {
        if (_currentState.ExtraState is PlayerAimState)
        {
            (_currentState.ExtraState as PlayerAimState).HandleThrowAnimationStart();
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
