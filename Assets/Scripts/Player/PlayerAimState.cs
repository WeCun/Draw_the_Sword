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
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    public override void Update()
    {
        base.Update();

        Time.timeScale = Mathf.Lerp(Time.timeScale, player.cooldownFactor,
            player.timeTransitionSpeed * Time.unscaledDeltaTime);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        
        if (Input.GetKeyUp(KeyCode.Mouse1) || !Input.GetKey(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        else
            player.SetVelocity(rb.velocity.x * 0.9f, rb.velocity.y);
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.x > player.transform.position.x && player.facingDir == -1)
            player.Flip();
        if(mousePos.x < player.transform.position.x && player.facingDir == 1)
            player.Flip();
    }
}
