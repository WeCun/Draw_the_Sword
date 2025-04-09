using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : PlayerAirState
{
    public PlayerDoubleJump(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.doubleJumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
