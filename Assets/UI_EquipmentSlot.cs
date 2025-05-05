using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equipmentType;

    public void OnValidate()
    {
        gameObject.name = "EquipmentSlot - " + equipmentType.ToString();
    }
}
