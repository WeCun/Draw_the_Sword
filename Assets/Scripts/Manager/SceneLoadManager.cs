using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;

    void Awake()
    {
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    
    
    public IEnumerator LoadGameSceneAsync(string _sceneName)
    {
        //调用 SceneManager.LoadSceneAsync 开始异步加载场景，返回 AsyncOperation 对象以监控加载状态
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);
        
        //当 allowSceneActivation = true 时，场景加载到 90% 后会自动进入激活阶段，进度直接跳至 100%
        asyncLoad.allowSceneActivation = false;

        
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log($"当前进度:{progress * 100}%");

            if (asyncLoad.progress >= 0.9f)
            {
                // 手动激活场景切换
                asyncLoad.allowSceneActivation = true;
            }
            
            // 每帧暂停，等待加载
            yield return null;
        }
    }
}
