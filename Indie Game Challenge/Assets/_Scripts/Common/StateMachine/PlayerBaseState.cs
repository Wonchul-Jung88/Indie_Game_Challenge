using UnityEngine;

public abstract class PlayerBaseState
{
    private bool _isRootState = false;
    private bool _isExtraState = false;
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSubState;
    private PlayerBaseState _currentExtraState;
    private PlayerBaseState _currentSuperState;

    public PlayerBaseState ExtraState { get { return _currentExtraState; } }
    public PlayerBaseState SubState { get { return _currentSubState; } }
    public PlayerBaseState SuperState { get { return _currentSuperState; } }

    protected bool IsRootState { set { _isRootState = value; } }
    protected bool IsExtraState { set { _isExtraState = value; } }
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();
    public abstract void InitializeExtraState();

    public void UpdateStates() {
        UpdateState();
        if (_currentSubState != null ) {
            _currentSubState.UpdateStates();
        }
        if (_currentExtraState != null) {
            _currentExtraState.UpdateState();
        }
    }

    protected void SwitchState(PlayerBaseState newState) {
        // current state exits state
        ExitState();

        // new state enters state
        newState.EnterState();

        if (_isRootState) {
            // switch current state of context
            _ctx.CurrentState = newState;
        }
        else if (_isExtraState) {
            _currentSuperState.SetExtraState(newState);
        }
        else if (_currentSuperState != null) {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState) {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState) {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    protected void SetExtraState(PlayerBaseState newExtraState)
    {
        _currentExtraState = newExtraState;
        newExtraState.SetSuperState(this);
    }
}
