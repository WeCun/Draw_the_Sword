using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigKingWalkState : PigKingGroundState
{
    public PigKingWalkState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _anim, PigKing _enemy) : base(_enemybase, _stateMachine, _anim, _enemy)
    {
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
        
        enemy.SetVelocity(enemy.walkSpeed * enemy.facingDir, enemy.rb.velocity.y);
        if(enemy.WallDetected() || !enemy.GroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
