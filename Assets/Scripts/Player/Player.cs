using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float moveSpeed;
    public float initialJumpForce;
    public float jumpForce;
    public float doubleJumpForce;
    public float maxJumpTime;
    public float dashSpeed;
    public float dashTime;
    public Vector2 wallJumpForce;
    public float wallJumpDuration;
    public int jumpCount = 1;
    public float cooldownFactor;
    public float timeTransitionSpeed;
    
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlide wallSlide { get; private set; }
    public PlayerWallJunpState wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerAimState aimState { get; private set; }
    public PlayerDoubleJumpState doubleJump { get; private set; }
    
    public float dashDir { get; private set; }
    
    protected  override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        fallState = new PlayerFallState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlide(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJunpState(this, stateMachine, "WallJump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        aimState = new PlayerAimState(this, stateMachine, "Aim");
        doubleJump = new PlayerDoubleJumpState(this, stateMachine, "Jump");
    }

    protected  override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    
    protected  override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;
            
            stateMachine.ChangeState(dashState);            
        }
    }
    
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinish();

    
    
}
