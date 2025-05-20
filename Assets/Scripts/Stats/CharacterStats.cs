using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum StatType
{
    maxHealth,
    damage,
    critChance,
    cirtPower,
    armor,
    evasion,
    strength,
    agility,
    vitality
}

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength;//增加1点伤害和暴击伤害
    public Stat agility;//增加1点暴击率和闪避率
    public Stat vitality;//增加3点最大生命值

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

    public float invincibleDuration;
    public Action onHealthChanged;
    protected bool isDead = false;

    public virtual void Start()
    {
        currentHealth = GetMaxHealth();
        isDead = false;
    }

    public void DoDamage(CharacterStats _target, float _damageMultiplier, Vector2 _knockbackPower, float _knockTime)
    {
        //判断此攻击会否会被闪避以及是否处于无敌状态
        //先判断是否无敌，再判断是否会被闪避
        if (_target.isDead || _target.isInvincible || TargetIsAvoid(_target))
            return;
        
        int totalDamage = Mathf.RoundToInt((strength.GetValue() + damage.GetValue()) * _damageMultiplier);
        //是否暴击
        if (IsCrit())
        {
           float totalCritPower = 0.1f * (strength.GetValue() + cirtPower.GetValue());
           float critDamage = totalCritPower * totalDamage;
           totalDamage = Mathf.RoundToInt(critDamage);
        }

        _target.StartCoroutine("SetInvincible");
        
        //受击FX
        _target.GetComponent<Entity>().SetKnockbackDir(transform);
        StartCoroutine(_target.GetComponent<Entity>().HitKnockback(_knockbackPower, _knockTime));    
        
        //使用类似于英雄联盟的护甲穿透    实际伤害 = 伤害 * (护甲 / (护甲 + 护甲常量))
        float _targetArmor = _target.armor.GetValue();
        totalDamage = Mathf.RoundToInt(totalDamage * (_targetArmor / (armorConstant + _targetArmor)));
        
        //造成伤害
        _target.TakeDamage(totalDamage);
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        
        //血量更改
        onHealthChanged?.Invoke();
        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
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
            _target.StartCoroutine("SetInvincible");
            return true;
        }

        return false;
    }

    public int GetStatValue(StatType statType)
    {
        if(statType == StatType.maxHealth) return maxHealth.GetValue();
        if(statType == StatType.damage) return damage.GetValue();
        if(statType == StatType.critChance) return critChance.GetValue();
        if(statType == StatType.cirtPower) return cirtPower.GetValue();
        if(statType == StatType.armor) return armor.GetValue();
        if(statType == StatType.evasion) return evasion.GetValue();
        if(statType == StatType.strength) return strength.GetValue();
        if(statType == StatType.agility) return agility.GetValue();
        if(statType == StatType.vitality) return vitality.GetValue();
        Debug.Log("123");
        return 0;
    }

    public int GetMaxHealth()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 3;
    }

    public virtual IEnumerator SetInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }
}
