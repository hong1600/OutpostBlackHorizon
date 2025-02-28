using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataLoader : MonoBehaviour
{
    public EnemyDataBase enemyDataBase;
    Table_Enemy enemyTable = new Table_Enemy();

    private void Awake()
    {
        enemyTable.Init_Binary("EnemyData");

        enemyDataBase = ScriptableObject.CreateInstance<EnemyDataBase>();

        foreach (var enemyInfo in enemyTable.Dictionary.Values)
        {
            EnemyData enemyData = ScriptableObject.CreateInstance<EnemyData>();

            enemyData.enemyID = enemyInfo.ID;
            enemyData.enemyName = enemyInfo.Name;
            enemyData.enemySpeed = enemyInfo.Speed;
            enemyData.enemyMaxHp = enemyInfo.MaxHp;
            enemyData.enemyHpBarPath = enemyInfo.HpBarPath;
            enemyData.enemyDecs = enemyInfo.Desc;

            enemyDataBase.enemyList.Add(enemyData);
        }
    }
}
