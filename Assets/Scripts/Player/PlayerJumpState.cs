using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    private float jumpTimer;
    private bool isJumping;
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.AddForce(Vector2.up * player.initialJumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;
        isJumping = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < player.maxJumpTime)
            {
                float forceMultiplier = 1 - (jumpTimer / player.maxJumpTime);
                //todo:由于update受帧数影响,有时候前面受力与下面的受力时间相差很小会导致突然跳的很高
                rb.AddForce(Vector2.up * forceMultiplier * player.jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                //Debug.Log("增加: " + Time.time);
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (rb.velocity.y < 0.05f)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
