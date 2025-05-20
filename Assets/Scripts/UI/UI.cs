using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject characterInformation;
    public GameObject settingUI;
    public GameObject[] inventory;
    
    public UI_ItemTip itemTip;
    public UI_EquipmentTip equipmentTip;
    [SerializeField] private ScreenFader screenFader;

    [SerializeField] private GameObject deadBackGround;
    [SerializeField] private TextMeshProUGUI deadText;
    [SerializeField] private float charDelay;
    [SerializeField] private GameObject deadTip;
    
    private bool canRestart = false;
    
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingUI.SetActive(!settingUI.activeSelf);
        }

        if (canRestart && Input.GetKeyDown(KeyCode.Space))
        {
            canRestart = false;
            //关闭死亡文字、提示、以及背景
            deadBackGround.SetActive(false);
            deadText.color = Color.clear;
            deadTip.SetActive(false);
            
            SaveManager.instance.DeleteSaveDate();
            StartCoroutine(RestartGame());
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

    public void GameOverOfDead()
    {
        deadBackGround.SetActive(true);
        string fullText = deadText.text;
        deadText.text = "";
        deadText.color = Color.red;
        StartCoroutine(ShowText(fullText, charDelay));
    }

    IEnumerator ShowText(string fullText, float delay)
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            deadText.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
        deadTip.SetActive(true);
        canRestart = true;
    }

    public IEnumerator RestartGame()
    {
        yield return screenFader.FadeIn();
        yield return StartCoroutine(SceneLoadManager.instance.LoadGameSceneAsync("MainScene"));
    }
    
    public IEnumerator ScreenFateOut()
    {
        yield return screenFader.FadeOut();
    }
    
    // public void LoadData(GameData _data)
    // {
    //     foreach (UI_VolumeSlider item in volumeSetting)
    //     {
    //         //通过PlayerPrefs来获取设置数据并且进行设置
    //         item.LoadVolume(PlayerPrefs.GetFloat(item.parameter, 1.0f));
    //     }
    //     
    //     // foreach (KeyValuePair<string, float> pair in _data.volumeSetting)
    //     // {
    //     //     foreach (UI_VolumeSlider item in volumeSetting)
    //     //     {
    //     //         if (item.parameter == pair.Key)
    //     //         {
    //     //             item.LoadVolume(pair.Value);
    //     //         }
    //     //     }
    //     // }
    // }
    //
    // public void SaveData(ref GameData _data)
    // {
    //     foreach (UI_VolumeSlider item in volumeSetting)
    //     {
    //         PlayerPrefs.SetFloat(item.parameter, item.slider.value);
    //     }
    //     PlayerPrefs.Save();
    //     
    //     // _data.volumeSetting.Clear();
    //     //
    //     // foreach (UI_VolumeSlider item in volumeSetting)
    //     // {
    //     //     _data.volumeSetting.Add(item.parameter, item.slider.value);
    //     // }
    // }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }
}
