using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    public override void Start()
    {
        base.Start();
        player = PlayerManager.instance.player;
    }

    public override void Die()
    {
        base.Die();
        isDead = true;
        player.stateMachine.ChangeState(player.dieState);
        StartCoroutine(player.DeadCanvas());
    }

    public override IEnumerator SetInvincible()
    {
        player.fx.InvokeRepeating("InvincibleBlink", 0,.1f);
        yield return base.SetInvincible();
        player.fx.CancelColorChange();
    }
}
