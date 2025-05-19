using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigKingRunState : EnemyState
{
    private PigKing enemy;
    private float turnTimer;
    private Transform player;
    private int runDir;
    public PigKingRunState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _anim, PigKing _enemy) : base(_enemybase, _stateMachine, _anim)
    {
        enemy = _enemy;
    }

    public override void Start()
    {
        base.Start();
        stateTimer = enemy.battleTime;
        player = PlayerManager.instance.player.transform;
        runDir = enemy.facingDir;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        turnTimer -= Time.deltaTime;
        if (player.position.x > enemy.transform.position.x && turnTimer < 0)
        {
            runDir = 1;
            turnTimer = enemy.turnTime;
        }
        if (player.position.x < enemy.transform.position.x && turnTimer < 0)
        {
            runDir = -1;
            turnTimer = enemy.turnTime;
        }
        enemy.SetVelocity(runDir * enemy.runSpeed, rb.velocity.y);
        
        if (enemy.PlayerDetected())
        {
            stateTimer = enemy.battleTime;
        }
        
        if (Vector2.Distance(player.position, enemy.attackPoint.position) <= enemy.attackRadius * 3)
        {
            if (enemy.DetectPlayer())
            {
                CharacterStats stat = PlayerManager.instance.player.stats;
                enemy.stats.DoDamage(stat, 1, enemy.knockBackForce, enemy.knockBackDuration);
            }
        }
        
        if(stateTimer < 0) 
            stateMachine.ChangeState(enemy.idleState);
    }
}
