using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
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
        
        //todo:明明在ground的进入跳跃状态的时候写了return 为啥还是会判断输入然后进行SetVelocity
        //当在 PlayerGroundState.Update() 中执行 return 时,只会退出 PlayerGroundState.Update()
        //方法不会阻止 PlayerMoveState.Update() 中后续代码的执行

        //解决方法
        if (stateMachine.currentState != this) return;
            
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        
        if(xInput == 0)
            stateMachine.ChangeState(player.idleState);
    }
}
