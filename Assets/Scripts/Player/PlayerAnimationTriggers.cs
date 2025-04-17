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

    public void AttackTrigger()
    {
        Collider2D[] collider = player.attackConfigs[player.primaryAttack.comboCounter].DetectTargets(player.attackCheck.position);
        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                
            }
        }
    }
    
    public void ThrowKuNai()
    {
        SkillManager.instance.kunai.CreateKuNai();
    }
}
