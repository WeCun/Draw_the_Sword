using System.Collections;
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
        //todo:这里就不能直接继承PlayState的的enter了，这里需要先把速度提上去再Setbool，不然初始的速度为0，会有一帧的动画是下降的，然后才是上升
        //应该也不是这个原因，在这帧加个player.anim.SetFloat("yVelocity", rb.velocity.y);更新yVelocity就可以了
        base.Enter();
        
        rb.velocity = new Vector2(rb.velocity.x, player.initialJumpForce);
        jumpTimer = 0;
        isJumping = true;
        
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (isJumping && Input.GetKey(KeyCode.Space))
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
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (rb.velocity.y < 0f)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
