using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equipmentType;
    public TextMeshProUGUI equipmentName;

    public void OnValidate()
    {
        gameObject.name = "EquipmentSlot - " + equipmentType.ToString();
    }
    
    public override void UpdateSlot()
    {
        base.UpdateSlot();
        if (item.data == null)
            equipmentName.gameObject.SetActive(true);
        else
            equipmentName.gameObject.SetActive(false);
    }
}
