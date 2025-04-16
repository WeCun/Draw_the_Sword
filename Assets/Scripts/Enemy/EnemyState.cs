using System;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected Rigidbody2D rb;
    private string anim;

    protected float stateTimer;

    public EnemyState(Enemy _enemybase, EnemyStateMachine _stateMachine, String _anim)
    {
        this.enemyBase = _enemybase;
        this.stateMachine = _stateMachine;
        this.anim = _anim;
    }
    
    public virtual void Start()
    {
        enemyBase.anim.SetBool(anim, true);
        rb = enemyBase.rb;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(anim, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    
}
