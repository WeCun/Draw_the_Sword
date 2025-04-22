using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine { get; private set; }
    
    protected  override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected  override void Start()
    {
        base.Start();
    }

    protected  override void Update()
    {
        
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void ChangeHitState(float _hitDuration)
    {
        
    }
    
    public override IEnumerator HitKnockback(Vector2 knockbackPower, float knockbackDuration)
    {
        ChangeHitState(knockbackDuration);
        return base.HitKnockback(knockbackPower, knockbackDuration);
    }
}
