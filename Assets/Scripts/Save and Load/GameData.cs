using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int currency;

    public SerializableDictionary<int, string> inventory;
    public SerializableDictionary<string, int> stash;
    public SerializableDictionary<string, int> stashPos;
    
    public Vector3 playerPos;

    public SerializableDictionary<string, float> volumeSetting;
    
    public GameData()
    {
        currency = 0;
        inventory = new SerializableDictionary<int, string>();
        stash = new SerializableDictionary<string, int>();
        stashPos = new SerializableDictionary<string, int>();
        playerPos = new Vector3(0, 0, 0);
        
        volumeSetting = new SerializableDictionary<string, float>();
    }
}
