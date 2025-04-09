using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlide wallSlide { get; private set; }
    public PlayerWallJunpState wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDis;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDis;
    
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    
    public float dashDir { get; private set; }
    
    void Awake()
    {
        stateMachine = new PlayerStateMachine();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        fallState = new PlayerFallState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlide(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJunpState(this, stateMachine, "WallJump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    
    void Update()
    {
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

    public bool GroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, whatIsGround);
    public bool WallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDis, whatIsGround);
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDis));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDis * facingDir, wallCheck.position.y));
    }

    public void SetZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        if(_xVelocity > 0 && !facingRight)
            Flip();
        if(_xVelocity < 0 && facingRight)
            Flip();
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    
    
}
