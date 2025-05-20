using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_StartMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private GameObject settingUI;
    

    private void Start()
    {
        if (!SaveManager.instance.HasSaveDate())
        {
            //禁止按钮交互
            continueButton.interactable = false;
            
            ColorBlock colors = continueButton.colors;
            colors.normalColor = new Color(0.5f, 0.5f, 0.5f, 1);
            continueButton.colors = colors;

            continueText.color = new Color(0.5f, 0.5f, 0.5f, 0.4f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            settingUI.SetActive(!settingUI.activeSelf);
    }

    public void ContinueGame()
    {
        StartCoroutine(TransitionScene(sceneName));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSaveDate();
        StartCoroutine(TransitionScene(sceneName));
    }

    public void End()
    {
        Debug.Log("退出游戏");
        Application.Quit();
    }
    
    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }

    //为什么上面从开始场景切换到游戏场景的时候需要用IEnumerator
    
    //Unity 的异步加载场景操作（通过 SceneManager.LoadSceneAsync）会将场景加载分为两个阶段：
    //资源加载阶段（0% ~ 90%）
    //加载场景中的资源（如模型、材质、脚本等），并在后台完成反序列化和内存分配
    //此阶段的进度由 asyncLoad.progress 表示，范围是0 到 0.9
    //场景激活阶段（90% ~ 100%）
    //将加载完成的场景设置为活动状态，触发 Unity 的生命周期事件（如 Awake、Start）

    private IEnumerator TransitionScene(string _sceneName)
    {
        yield return screenFader.FadeIn();
        yield return StartCoroutine(SceneLoadManager.instance.LoadGameSceneAsync(_sceneName));
    }
}
