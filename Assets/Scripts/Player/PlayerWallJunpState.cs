using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJunpState : PlayerState
{
    
    
    public PlayerWallJunpState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.wallJumpDuration;
        player.SetVelocity(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
            stateMachine.ChangeState(player.fallState);
        
        if(player.GroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
