using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttackController
{
    private Transform attackCheck;
    private Player player;
    public PlayerAttackConfig attackConfig;
    public float moveTimer;
    
    public PlayerAttackController()
    {
        player = PlayerManager.instance.player;
        attackCheck = player.attackCheck;
    }

    public void AttackStart(PlayerAttackConfig _attackConfig)
    {
        attackConfig = _attackConfig;
        player.isAttacking = true;
        float attackDir = player.facingDir;
        float xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0) attackDir = xInput;
        player.SetVelocity(_attackConfig.movement.x * attackDir, _attackConfig.movement.y);
        moveTimer = _attackConfig.movementTime;
    }
    
    public Collider2D[] DectectTargets(PlayerAttackConfig _attackConfig)
    {
        attackConfig = _attackConfig;
        switch(attackConfig.shape)
        {
            case AttackShape.Circle:
                return CircleDetectTargets(_attackConfig);
            case AttackShape.Rectangle:
                return RectangleDetectTargets(_attackConfig);
            default:
                return null;
        }
    }

    private Collider2D[] CircleDetectTargets(PlayerAttackConfig attackConfig)
    {
        Vector2 attackDir = new Vector2(attackCheck.position.x + attackConfig.offsetX, attackCheck.position.y + attackConfig.offsetY);
        return Physics2D.OverlapCircleAll(attackDir, attackConfig.range);
    }

    private Collider2D[] RectangleDetectTargets(PlayerAttackConfig attackConfig)
    {
        Vector2 attackDir = new Vector2(attackCheck.position.x + attackConfig.offsetX, attackCheck.position.y + attackConfig.offsetY);
        return Physics2D.OverlapBoxAll(attackDir, attackConfig.size, 0f);
    }
}
