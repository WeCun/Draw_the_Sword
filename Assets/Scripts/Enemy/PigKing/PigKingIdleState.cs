using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigKingIdleState : EnemyState
{
    private PigKing enemy;
    public PigKingIdleState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _anim, PigKing _enemy) : base(_enemybase, _stateMachine, _anim)
    {
        enemy = _enemy;
    }

    public override void Start()
    {
        base.Start();
        rb.velocity = new Vector2(0, 0);
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
            stateMachine.ChangeState(enemy.walkState);
    }
}
