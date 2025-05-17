using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour, ISaveManager
{
    public GameObject characterInformation;
    public GameObject[] inventory;
    
    public UI_ItemTip itemTip;
    public UI_EquipmentTip equipmentTip;
    [SerializeField] private ScreenFader screenFader;

    [SerializeField] public UI_VolumeSlider[] volumeSetting;
    
    private void Start()
    {
        StartCoroutine(ScreenFateOut());
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


    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.volumeSetting)
        {
            foreach (UI_VolumeSlider item in volumeSetting)
            {
                if (item.parameter == pair.Key)
                {
                    item.LoadVolume(pair.Value);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.volumeSetting.Clear();
        
        foreach (UI_VolumeSlider item in volumeSetting)
        {
            _data.volumeSetting.Add(item.parameter, item.slider.value);
        }
    }
}
