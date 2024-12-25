using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public interface ITableMgr
{
    void Init(ETable Name);
    void Save(ETable Name);
}

public class TableMgr : ITableMgr
{
    public Table_Unit unit = new Table_Unit();
    public Table_Enemy enemy = new Table_Enemy();
    public Table_Item item = new Table_Item();

    public void Init(ETable Name)
    {
        switch (Name)
        {
            case ETable.UnitData:
#if UNITY_EDITOR
                unit.Init_Csv(ETable.UnitData, 1, 0);
#else
        unit.Init_Binary($"{Name}");
#endif
                break;
            case ETable.EnemyData:
#if UNITY_EDITOR
                enemy.Init_Csv(ETable.EnemyData, 1, 0);
#else
        enemy.Init_Binary($"{Name}");
#endif
                break;
            case ETable.ItemData:
#if UNITY_EDITOR
                item.Init_Csv(ETable.ItemData, 1, 0);
#else
        item.Init_Binary($"{Name}");
#endif
                break;

        }
    }

    public void Save(ETable Name)
    {
        switch (Name) 
        {
            case ETable.UnitData:
                unit.Save_Binary("UnitData");
                break;
            case ETable.EnemyData:
                enemy.Save_Binary("EnemyData");
                break;
            case ETable.ItemData:
                item.Save_Binary("ItemData");
                break;
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
