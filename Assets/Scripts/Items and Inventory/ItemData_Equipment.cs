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
}
