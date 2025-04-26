using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet
}

[CreateAssetMenu(fileName = "Item", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
}
