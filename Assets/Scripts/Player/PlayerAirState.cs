using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerExtra
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
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

        if (Input.GetKeyDown(KeyCode.Space) && player.jumpCount > 0)
        {
            player.jumpCount--;
            stateMachine.ChangeState(player.doubleJump);
        }
        
        if (xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        }
        else
        {
            //todo:为什么在tilemap原地跳跃的时候，水平方向会有速度的变化，只能被迫把速度改为0了
            player.SetVelocity(0, rb.velocity.y);
        }

        if (player.WallDetected() && xInput != 0 && xInput == player.facingDir)
        {
            stateMachine.ChangeState(player.wallSlide);
        }
    }

    // private void AirMove()
    // {
    //     
    //     rb.position = new Vector2(rb.position.x + xInput * player.moveSpeed * Time.deltaTime, rb.position.y);
    //     if(xInput == 1 && player.facingDir == -1)
    //         player.Flip();
    //     if(xInput == -1 && player.facingDir == 1)
    //         player.Flip();
    // }
}
