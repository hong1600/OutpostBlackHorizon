using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TableEditor : MonoBehaviour
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
        ParseTableCsv(ETable.UNITDATA);
    }

    [MenuItem("CSV_Util/Table/ItemData &F1", false, 1)]
    static public void ParserItemCsv()
    {
        ParseTableCsv(ETable.ITEMDATA);
    }

    [MenuItem("CSV_Util/Table/EnemyData &F1", false, 1)]
    static public void ParserEnemyCsv()
    {
        ParseTableCsv(ETable.ENEMYDATA);
    }

    [MenuItem("CSV_Util/Table/CameraData &F1", false, 1)]
    static public void ParserCameraCsv()
    {
        ParseTableCsv(ETable.CAMERADATA);
    }

}
