using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigKingGroundState : EnemyState
{
    protected PigKing enemy;
    
    public PigKingGroundState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _anim, PigKing _enemy) : base(_enemybase, _stateMachine, _anim)
    {
        enemy = _enemy;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.runState);
    }
}
