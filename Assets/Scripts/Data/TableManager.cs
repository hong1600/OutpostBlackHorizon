using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableManager
{
    public TableUnit unit = new TableUnit();
    public Table_UnitName unitName = new Table_UnitName();
    public Table_UnitSkill unitSkill = new Table_UnitSkill();
    public TableEnemy enemy = new TableEnemy();
    public TableItem item = new TableItem();
    public TableTurret turret = new TableTurret();
    public TableField field = new TableField();
    public TableMap map = new TableMap();

    public void Init(ETable _name)
    {
        switch (_name)
        {
            case ETable.UNITDATA:
#if UNITY_EDITOR
                unit.Init_Csv(ETable.UNITDATA, 1, 0);
#else
        unit.Init_Binary($"{_name}");
#endif
                break;
            case ETable.UNITNAME:
#if UNITY_EDITOR
                unitName.Init_Csv(ETable.UNITNAME, 1, 0);
#else
        unit.Init_Binary($"{_name}");
#endif
                break;
            case ETable.UNITSKILL:
#if UNITY_EDITOR
                unitSkill.Init_Csv(ETable.UNITSKILL, 1, 0);
#else
        unit.Init_Binary($"{_name}");
#endif
                break;
            case ETable.ENEMYDATA:
#if UNITY_EDITOR
                enemy.Init_Csv(ETable.ENEMYDATA, 1, 0);
#else
        enemy.Init_Binary($"{_name}");
#endif
                break;
            case ETable.ITEMDATA:
#if UNITY_EDITOR
                item.Init_Csv(ETable.ITEMDATA, 1, 0);
#else
        item.Init_Binary($"{_name}");
#endif
                break;
            case ETable.TURRETDATA:
#if UNITY_EDITOR
                turret.Init_Csv(ETable.TURRETDATA, 1, 0);
#else
        unit.Init_Binary($"{_name}");
#endif
                break;
            case ETable.FIELDDATA:
#if UNITY_EDITOR
                field.Init_Csv(ETable.FIELDDATA, 1, 0);
#else
        unit.Init_Binary($"{_name}");
#endif
                break;
            case ETable.MAPDATA:
#if UNITY_EDITOR
                map.Init_Csv(ETable.MAPDATA, 1, 0);
#else
        unit.Init_Binary($"{_name}");
#endif
                break;
        }
    }

    public void Save(ETable _name)
    {
        switch (_name) 
        {
            case ETable.UNITDATA:
                unit.Save_Binary("UnitData");
                break;
            case ETable.UNITNAME:
                unitName.Save_Binary("UnitName");
                break;
            case ETable.UNITSKILL:
                unitSkill.Save_Binary("UnitSkill");
                break;
            case ETable.ENEMYDATA:
                enemy.Save_Binary("EnemyData");
                break;
            case ETable.ITEMDATA:
                item.Save_Binary("ItemData");
                break;
            case ETable.TURRETDATA:
                turret.Save_Binary("TurretData");
                break;
            case ETable.FIELDDATA:
                field.Save_Binary("FieldData");
                break;
            case ETable.MAPDATA:
                map.Save_Binary("MapData");
                break;
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
