using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Entity
{
    public float moveSpeed;
    
    [Header("Jump")]
    public float initialJumpForce;
    public float jumpForce;
    
    [Header("DoubleJump")]
    public float doubleJumpForce;
    public float maxJumpTime;
    public int jumpCount = 1;
    
    [Header("Dash")]
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private float dashTimer = 0;
    
    [Header("Wall Jump")]
    public Vector2 wallJumpForce;
    public float wallJumpDuration;
    
    [Header("TimeCooldown")]
    public float cooldownFactor;
    public float timeTransitionSpeed;
    
    public PlayerAttackController attackController;
    public bool isAttacking = false;

    
    #region State
    
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
    public PlayerDieState dieState { get; private set; }
    #endregion
    public float dashDir { get; private set; }
    public Transform attackCheck;
    public Vector2 groundCheckSize;
    
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
        dieState = new PlayerDieState(this, stateMachine, "Die");
    }

    protected  override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        attackController = new PlayerAttackController();
    }

    
    protected  override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0)
        {
            dashTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;
            
            stateMachine.ChangeState(dashState);            
        }
    }
    
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinish();

    public override bool GroundDetected()
    {
        //利用长方形进行地面检测
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, groundCheckSize, 0, whatIsGround);
        if (colliders.Length > 0)
        {
            return true;
        }
        
        return false;
    }
    
    public IEnumerator DeadCanvas()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Find("Canvas").GetComponent<UI>().GameOverOfDead();
    }
    
    protected override void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDis * facingDir, wallCheck.position.y));
        
        //地面检测
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        
        
        if (isAttacking)
        {
            Gizmos.color = Color.red;
            Vector2 position = new Vector2(attackCheck.position.x + attackController.attackConfig.offsetX, attackCheck.position.y + attackController.attackConfig.offsetY);
            switch (attackController.attackConfig.shape)
            {
                case AttackShape.Circle:
                    Gizmos.DrawWireSphere(position, attackController.attackConfig.range);
                    break;
                case AttackShape.Rectangle:
                    Gizmos.DrawWireCube(position, attackController.attackConfig.size);
                    break;
            }
        }

    }
}
