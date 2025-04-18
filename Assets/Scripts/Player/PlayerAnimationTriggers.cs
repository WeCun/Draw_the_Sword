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
        player.attackController.DectectTargets(attackConfig);
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
