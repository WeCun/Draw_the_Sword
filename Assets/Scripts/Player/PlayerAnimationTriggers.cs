using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    public void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    public void AttackTrigger(PlayerAttackConfig attackConfig)
    {
        Collider2D[] colliders = player.attackController.DectectTargets(attackConfig);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats enemyStats = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(enemyStats, attackConfig.damageMultiplier, attackConfig.knockbackPower);
            }
            
        }
    }

    public void AttackStart(PlayerAttackConfig attackConfig)
    {
        player.attackController.AttackStart(attackConfig);
    }
    
    public void ThrowKuNai()
    {
        SkillManager.instance.kunai.CreateKuNai();
    }
}
