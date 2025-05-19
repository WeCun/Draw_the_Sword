using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.stats.isInvincible = true;
        
        stateTimer = player.dashTime;
    }

    public override void Exit()
    {
        base.Exit();
        player.stats.isInvincible = false;
    }

    public override void Update()
    {
        base.Update();
        
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        
        if(stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
