using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.x > player.transform.position.x && player.facingDir == -1)
            player.Flip();
        if(mousePos.x < player.transform.position.x && player.facingDir == 1)
            player.Flip();
    }
}
