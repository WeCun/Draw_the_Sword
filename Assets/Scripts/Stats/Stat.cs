using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//标记一个类，结构体和枚举可以被序列化
[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;
    public List<int> modifiers;

    public int GetValue()
    {
        int finnalValue = baseValue;

        foreach (var modifier in modifiers)
        {
            finnalValue += modifier;
        }

        return finnalValue;
    }

    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }

    public void AddModifier(int _value)
    {
        modifiers.Add(_value);
    }

    public void RemoveModifier(int _value)
    {
        modifiers.Remove(_value);
    }
    
}
