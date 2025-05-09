using System.Text;
using UnityEngine;

public enum EquipmentType
{
    Weapon,  //武器
    Armor,   //盔甲
    Amulet,  //护身符
    Potion   //药水
}

[CreateAssetMenu(fileName = "Item", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    public StringBuilder sb = new StringBuilder();
    
    [Header("basic stats")]
    public int maxHealth;
    public int armor;
    public int evasion;
    
    [Header("damage")]
    public int damage;
    public int critChance;
    public int cirtPower;
    

    //添加装备给的加成
    public void AddModifies()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.cirtPower.AddModifier(cirtPower);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
    }

    //删除装备给的加成
    public void RemoveModifies()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.cirtPower.RemoveModifier(cirtPower);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
    }

    //获取装备加成描述
    public string GetEquipmentStatDes()
    {
        sb.Clear();//似乎与sb.Length = 0等价
        
        AddStatDes("maxHealth", maxHealth);
        AddStatDes("armor", armor);
        AddStatDes("evasion", evasion);
        AddStatDes("damage", damage);
        AddStatDes("critChance", critChance);
        AddStatDes("cirtPower", cirtPower);
        
        
        return sb.ToString();
    }

    //获取属性加成描述
    private void AddStatDes(string statName, int _value)
    {
        if (_value != 0)
        {
            if (sb.Length != 0) sb.AppendLine();
            
            string c = "+ ";
            if (_value < 0)
            {
                c = "- ";
                _value = -_value;
            }
            else c = "+ ";
            
            
            sb.Append(c + _value + " " + statName);
        }
    }
}
