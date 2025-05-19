using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class PigKing : Enemy
{
    public float idleTime;
    
    public float battleTime;
    public float runSpeed;
    public float turnTime;
    public float hitDuration;

    [Header("Walk")]
    public float walkTime;
    public float walkSpeed;
    
    [Header("Attack")]
    public int damage;
    public Vector2 knockBackForce;
    public float knockBackDuration;
    
    
    public PigKingIdleState idleState { get; private set; }
    public PigKingWalkState walkState { get; private set; }
    public PigKingRunState runState { get; private set; }
    public PigKingHitState hitState { get; private set; }
    public PigKingDieState dieState { get; private set; }
    public LayerMask whatIsPlayer;
    public float playerCheckDis;
    
    public Transform attackPoint;
    public float attackRadius;
    
    protected override void Awake()
    {
        base.Awake();
        idleState = new PigKingIdleState(this, stateMachine, "Idle", this);
        walkState = new PigKingWalkState(this, stateMachine, "Walk", this); 
        runState = new PigKingRunState(this, stateMachine, "Run", this);
        hitState = new PigKingHitState(this, stateMachine, "Hit", this);
        dieState = new PigKingDieState(this, stateMachine, "Die", this);
        
        facingRight = false;
        facingDir = -1;
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    
    protected override void Update()
    {
        base.Update();
    }

    public override void ChangeHitState(float _hitDuration)
    {
        hitDuration = _hitDuration;
        stateMachine.ChangeState(hitState);
    }
    
    public bool PlayerDetected() => Physics2D.Raycast(wallCheck.position + new Vector3(0, .1f), Vector2.right * facingDir, playerCheckDis, whatIsPlayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position+ new Vector3(0, .15f), new Vector3(wallCheck.position.x + playerCheckDis * facingDir, wallCheck.position.y + 0.15f));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public bool DetectPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsPlayer);
        if (collider != null)
        {
            return true;
        }

        return false;
    }
}
