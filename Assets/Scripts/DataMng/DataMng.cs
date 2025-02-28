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
    [SerializeField] UserDataLoader userDataLoader;

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
        UserDataLoader = userDataLoader;
    }

    public UnitDataLoader UnitDataLoader { get; private set; }
    public EnemyDataLoader EnemyDataLoader { get; private set; }
    public UserDataLoader UserDataLoader { get; private set; }
}

