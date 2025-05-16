using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;
    
    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
        
        saveManagers = FindAllSaveManagers();
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
    }

    private void Start()
    {
        LoadGame();
    }

    private void NewGame()
    {
        gameData = new GameData();
    }

    private void LoadGame()
    {
        //game data = data from data handle
        gameData = dataHandler.Load();
        
        if (gameData == null)
        {
            NewGame();
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
    }

    private void SaveGame()
    {
        //把这里的总gameData传递到各个需要保存数据的地方进行数据保存
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        if (saveManagers.Count > 0)
        {
            //data handle save gameData
            Debug.Log(saveManagers);
            foreach (ISaveManager saveManager in saveManagers)
            {
                Debug.Log(saveManager);
            }
            dataHandler.Save(gameData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame(); 
    }

    private List<ISaveManager> FindAllSaveManagers()    
    {
        //FindObjectsOfType<MonoBehaviour>：获取场景中所有 MonoBehaviour 组件
        //OfType<ISaveManager>：筛选出实现了 ISaveManager 接口的 MonoBehaviour 组件（Linq方法）
        //todo:FindObjectsOfType<>只能查找继承自 Component 的类型，不能直接去查找ISaveManager
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

    [ContextMenu("Delete data file")]
    public void DeleteSaveDate()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        dataHandler.Delete();
    }
    
    public bool HasSaveDate()
    {
        if (dataHandler.Load() == null)
        {
            return false;
        }

        return true;
    }
}
