using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength;//增加1点伤害和暴击伤害
    public Stat agility;//增加1点暴击率和闪避率
    public Stat Vitality;//增加3点最大生命值

    [Header("Offensive stats")] 
    public Stat damage;
    public Stat critChance;
    public Stat cirtPower;
    public Stat armorpenetration; // 护甲穿透
    
    [Header("Defensive stats")]
    public Stat armor;
    public Stat evasion;
    public Stat maxHealth;

    public int armorConstant;
    public int currentHealth;
    public bool isInvincible;

    public void DoDamage(CharacterStats _target, float _damageMultiplier, Vector2 _knockbackPower, float _knockTime)
    {
        if (TargetIsAvoid(_target) || _target.isInvincible)
            return;
        
        int totalDamage = Mathf.RoundToInt((strength.GetValue() + damage.GetValue()) * _damageMultiplier);
        if (IsCrit())
        {
           float totalCritPower = 0.1f * (strength.GetValue() + cirtPower.GetValue());
           float critDamage = totalCritPower * totalDamage;
           totalDamage = Mathf.RoundToInt(critDamage);
        }
        
        _target.GetComponent<Entity>().SetKnockbackDir(transform);
        StartCoroutine(_target.GetComponent<Entity>().HitKnockback(_knockbackPower, _knockTime));    
        
        float _targetArmor = _target.armor.GetValue();
        totalDamage = Mathf.RoundToInt(totalDamage * (_targetArmor / (armorConstant + _targetArmor)));
        
        _target.TakeDamage(totalDamage);
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Die");
    }

    private bool IsCrit()
    {
        int totalCritChance = agility.GetValue() + critChance.GetValue();
        if (Random.Range(0, 100) < totalCritChance)
        {
            return true;
        }

        return false;
    }

    private bool TargetIsAvoid(CharacterStats _target)
    {
        int totalEvasion = _target.agility.GetValue() + _target.evasion.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("Miss!");
            return true;
        }

        return false;
    }
}
