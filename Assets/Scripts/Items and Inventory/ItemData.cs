using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    public string itemId;

    //在 Inspector 中修改脚本数值
    //通过脚本加载/重新导入资源时
    //在编辑器中进行撤销/重做操作时
    private void OnValidate()
    {
        //将相对路径转换为GUID
        
#if UNITY_EDITOR
        //返回指定对象的完整工程相对路径
        //Assets/[子目录]/[文件名].[扩展名], 例如Assets/Resources/MyScript.cs
        string path = AssetDatabase.GetAssetPath(this);
        //将 Unity 工程相对路径转换为全局唯一标识符
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    //只能放在string上，可以将单行输入框，转变为多行文本区域
    [TextArea]
    public string description;
}
