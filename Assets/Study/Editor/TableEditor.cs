using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TableEditor : MonoBehaviour
{
    static public TableManager tableManager;

    static public void Init()
    {
        if (tableManager == null)
        {
            tableManager = new TableManager();
        }
    }

    static public void ParseTableCsv(ETable _tableName)
    {
        Init();
        tableManager.Init(_tableName);
        tableManager.Save(_tableName);
    }

    [MenuItem("CSV_Util/Table/UnitData/Parse Unit Data CSV &F1", false, 1)]
    static public void ParserUnitCsv()
    {
        ParseTableCsv(ETable.UNITDATA);
    }

    [MenuItem("CSV_Util/Table/UnitData/Parse Unit Name CSV &F2", false, 1)]
    static public void ParserUnitNameCsv()
    {
        ParseTableCsv(ETable.UNITNAME);
    }

    [MenuItem("CSV_Util/Table/UnitData/Parse Unit Skill CSV &F3", false, 1)]
    static public void ParserUnitSkillCsv()
    {
        ParseTableCsv(ETable.UNITSKILL);
    }

    [MenuItem("CSV_Util/Table/ItemData/Parse Item CSV &F4", false, 1)]
    static public void ParserItemCsv()
    {
        ParseTableCsv(ETable.ITEMDATA);
    }

    [MenuItem("CSV_Util/Table/EnemyData/Parse Enemy CSV &F5", false, 1)]
    static public void ParserEnemyCsv()
    {
        ParseTableCsv(ETable.ENEMYDATA);
    }

    [MenuItem("CSV_Util/Table/CameraData/Parse Camera CSV &F6", false, 1)]
    static public void ParserCameraCsv()
    {
        ParseTableCsv(ETable.CAMERADATA);
    }
}
