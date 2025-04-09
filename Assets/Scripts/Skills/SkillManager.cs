using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        //todo:删掉旧的instance后，新的instance是怎么赋值的
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    
    
}
