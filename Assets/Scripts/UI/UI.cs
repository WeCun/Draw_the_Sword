using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject characterInformation;
    public GameObject[] inventory;
    
    public UI_ItemTip itemTip;
    public UI_EquipmentTip equipmentTip;
    [SerializeField] private ScreenFader screenFader;

    private void Start()
    {
        //StartCoroutine(ScreenFateOut());
        //screenFader.FadeOut();
    }

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

    public IEnumerator ScreenFateOut()
    {
        yield return screenFader.FadeOut();
    }
}
