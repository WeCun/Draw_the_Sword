using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject characterInformation;
    public GameObject[] inventory;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            characterInformation.SetActive(!characterInformation.activeSelf);
        }
    }

    public void SwitchInventory(GameObject targetInventory)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == targetInventory)
            {
                inventory[i].SetActive(true);
            }
            else
            {
                inventory[i].SetActive(false);
            }
        }
        
        Inventory.instance.UpdateSlot();
    }
}
