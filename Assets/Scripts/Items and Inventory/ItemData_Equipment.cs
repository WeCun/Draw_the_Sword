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
}
