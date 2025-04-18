using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public int comboCounter { get; private set; } = 0;
    private float lastAttackTime;
    private float comboWindow = 1;
    
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time - lastAttackTime > comboWindow)
            comboCounter = 0;
        
        player.anim.SetInteger("ComboCounter", comboCounter);

        /*xInput = 0;
        xInput = Input.GetAxisRaw("Horizontal");
        float attackDir = player.facingDir;
        if (xInput != 0) attackDir = xInput;*/

    }

    public override void Exit()
    {
        base.Exit();

        player.isAttacking = false;
        comboCounter++;
        lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        player.attackController.moveTimer -= Time.deltaTime;
        if(player.attackController.moveTimer < 0)
            player.SetZeroVelocity();
        
        if(animTrigger)
            stateMachine.ChangeState(player.idleState);
    }
}
