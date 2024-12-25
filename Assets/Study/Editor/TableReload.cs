using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TableReload : MonoBehaviour
{
    static public TableMgr tableMgr;
    static public ITableMgr iTableMgr;

    static public void Init()
    {
        if (tableMgr == null)
        {
            tableMgr = new TableMgr();
            iTableMgr = tableMgr;
        }
    }

    static public void ParseTableCsv(ETable tableName)
    {
        Init();
        iTableMgr.Init(tableName);
        iTableMgr.Save(tableName);
    }

    [MenuItem("CSV_Util/Table/UnitData &F1", false, 1)]
    static public void ParserUnitCsv()
    {
        ParseTableCsv(ETable.UnitData);
    }

    [MenuItem("CSV_Util/Table/ItemData &F1", false, 1)]
    static public void ParserItemCsv()
    {
        ParseTableCsv(ETable.ItemData);
    }

    [MenuItem("CSV_Util/Table/EnemyData &F1", false, 1)]
    static public void ParserEnemyCsv()
    {
        ParseTableCsv(ETable.EnemyData);
    }

}
