using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundState : PlayerExtra
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.jumpCount = 1;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && player.GroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }
        
         if(Input.GetKeyDown(KeyCode.Mouse0))
             stateMachine.ChangeState(player.primaryAttack);
            
        
        if(!player.GroundDetected())
            stateMachine.ChangeState(player.fallState);
    }
}
