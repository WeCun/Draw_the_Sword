using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int currency;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void LoadData(GameData _data)
    {
        currency = _data.currency;
        player.transform.position = _data.playerPos;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = currency;
        _data.playerPos = player.transform.position;
    }
}
