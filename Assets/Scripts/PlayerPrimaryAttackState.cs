using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
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
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        
        if(animTrigger)
            stateMachine.ChangeState(player.idleState);
    }
}
