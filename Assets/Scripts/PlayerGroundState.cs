using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Jump") && player.GroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }
        
        if(!player.GroundDetected())
            stateMachine.ChangeState(player.fallState);
    }
}
