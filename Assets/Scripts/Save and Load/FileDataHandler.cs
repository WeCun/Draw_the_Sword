using System;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath;
    private string dataFileName;

    public FileDataHandler(string _dataDirPath, string _dataFileName)
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName;
    }

    public void Save(GameData _data)
    {
        //Path.Combine()：安全拼接路径，自动处理不同操作系统的路径分隔符（\ 或 /）
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //Path.GetDirectoryName(fullPath)：提取文件路径的目录部分
            //Directory.CreateDirectory()：创建目录，如果目录已存在则不会报错
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //JsonUtility.ToJson()：Unity 内置方法，将可序列化对象（如 [Serializable] 标记的类）转换为 JSON 字符串。
            //prettyPrint: true：格式化 JSON（添加缩进和换行），方便人类阅读和调试。
            string dataToStore = JsonUtility.ToJson(_data, true);
            
            //FileStream：文件流，用于读写文件
            //FileMode.Create：若文件存在则覆盖，不存在则新建。
            //using 语句：确保 FileStream 在代码块结束后自动释放资源（即使发生异常）。
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                //StreamWriter：专门用于写入文本的类，支持编码（如 UTF-8）。
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    //writer.Write()：将 JSON 字符串写入文件流。
                    writer.Write(dataToStore);
                }
            }
            
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving file: " + fullPath + "\n" + e);
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader((stream)))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading file: " + fullPath + "\n" + e);
            }
        }
        
        return loadData;
    }

    //删除存储数据文件
    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        
        if(File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
