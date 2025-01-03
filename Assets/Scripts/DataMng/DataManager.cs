using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataMng : MonoBehaviour
{
    public static DataMng instance;

    public PlayerDataLoader playerDataLoader;
    public DataSaveLoader dataSaveLoader;
    public UnitDataLoader unitDataLoader;
    public ItemDataLoader itemDataLoader;

    public PlayerData playerData = new PlayerData();
    public List<ItemData> itemDataList = new List<ItemData>();
    public List<UnitData> unitDataList = new List<UnitData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        playerDataLoader = new PlayerDataLoader();
        dataSaveLoader = new DataSaveLoader();
        unitDataLoader = new UnitDataLoader();
        itemDataLoader = new ItemDataLoader();

        dataSaveLoader.LoadData();
        playerDataLoader.Initialize();
    }
}

