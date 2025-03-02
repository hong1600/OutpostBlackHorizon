using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataMng : MonoBehaviour
{
    public static DataMng instance;

    [SerializeField] UserDataLoader userDataLoader;

    TableUnit tableUnit;
    TableEnemy tableEnemy;
    TableItem tableItem;

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

        UserDataLoader = userDataLoader;

        tableUnit = new TableUnit();
        tableUnit.Init_Binary("UnitData");
        TableUnit = tableUnit;

        tableEnemy = new TableEnemy();
        tableEnemy.Init_Binary("EnemyData");
        TableEnemy = tableEnemy;

        tableItem = new TableItem();
        tableItem.Init_Binary("ItemData");
        TableItem = tableItem;
    }

    public UserDataLoader UserDataLoader { get; private set; }
    public TableUnit TableUnit { get; private set; }
    public TableEnemy TableEnemy { get; private set; }
    public TableItem TableItem { get; private set; }
}

