using System.Collections.Generic;

enum PlayerStates
{
    idle,
    walk,
    run,
    grounded,
    jump,
    fall,
    attack,
    aim,
    talk,
}

public class PlayerStateFactory
{
    PlayerStateMachine _context;
    Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

    public PlayerStateFactory( PlayerStateMachine currentContext )
    {
        _context = currentContext;
        _states[PlayerStates.idle] = new PlayerIdleState(_context, this);
        _states[PlayerStates.walk] = new PlayerWalkState(_context, this);
        _states[PlayerStates.run] = new PlayerRunState(_context, this);
        _states[PlayerStates.jump] = new PlayerJumpState(_context, this);
        _states[PlayerStates.grounded] = new PlayerGroundedState(_context, this);
        _states[PlayerStates.fall] = new PlayerFallState(_context, this);
        _states[PlayerStates.attack] = new PlayerAttackState(_context, this);
        _states[PlayerStates.aim] = new PlayerAimState(_context, this);
        _states[PlayerStates.talk] = new PlayerTalkingState(_context, this);
    }

    public PlayerBaseState Idle() {
        return _states[PlayerStates.idle];
    }
    public PlayerBaseState Walk() {
        return _states[PlayerStates.walk];
    }
    public PlayerBaseState Run() {
        return _states[PlayerStates.run];
    }
    public PlayerBaseState Grounded() {
        return _states[PlayerStates.grounded];
    }
    public PlayerBaseState Jump() {
        return _states[PlayerStates.jump];
    }
    public PlayerBaseState Fall() {
        return _states[PlayerStates.fall];
    }
    public PlayerBaseState Attack() {
        return _states[PlayerStates.attack];
    }
    public PlayerBaseState Aim() {
        return _states[PlayerStates.aim];
    }
    public PlayerBaseState Talk()
    {
        return _states[PlayerStates.talk];
    }
}
