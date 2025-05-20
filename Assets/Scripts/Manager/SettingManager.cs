using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;
    
    public UI_VolumeSlider[] volumeSetting;
    
    public void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
        
        LoadData();
    }
    
    //对象禁用或场景卸载才会触发
    public void OnDisable()
    {
        SaveData();
    }
    
    public void LoadData()
    {
        foreach (UI_VolumeSlider item in volumeSetting)
        {
            //通过PlayerPrefs来获取设置数据并且进行设置
            item.LoadVolume(PlayerPrefs.GetFloat(item.parameter, 1.0f));
        }
    }

    public void SaveData()
    {
        foreach (UI_VolumeSlider item in volumeSetting)
        {
            PlayerPrefs.SetFloat(item.parameter, item.slider.value);
        }
        
        PlayerPrefs.Save();
    }
}
