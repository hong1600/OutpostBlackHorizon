using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataMng : MonoBehaviour
{
    public static DataMng instance;

    [SerializeField] UnitDataLoader unitDataLoader;
    [SerializeField] EnemyDataLoader enemyDataLoader;

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

        UnitDataLoader = unitDataLoader;
        EnemyDataLoader = enemyDataLoader;
    }

    public PlayerDataLoader PlayerDataLoader { get; private set; }
    public DataSaveLoader DataSaveLoader { get; private set; }
    public UnitDataLoader UnitDataLoader { get; private set; }
    public EnemyDataLoader EnemyDataLoader { get; private set; }
    public ItemDataLoader ItemDataLoader { get; private set; }
}

