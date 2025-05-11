using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
   void LoadData(GameData _data);
   //todo:ref按引用传递参数,ref调用方必须初始化变量
   void SaveData(ref GameData _data);
}
