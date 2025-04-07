using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected float xInput;
    protected float yInput;
    protected string animName;
    
    protected Rigidbody2D rb;

    protected float stateTimer;
    
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animName = _animName;
    }
    
    public virtual void Enter()
    {
        player.anim.SetBool(animName, true);
        rb = player.rb;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        //Input.GetAxis()得到的值会有一个平滑的过程
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }
}
