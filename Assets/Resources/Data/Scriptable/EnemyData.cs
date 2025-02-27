using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptble Object/EnemyData")]

public class EnemyData : ScriptableObject
{
    public int enemyID;
    public string enemyName;
    public float enemySpeed;
    public float enemyMaxHp;
    public string enemyHpBarPath;
    public string enemyDecs;
    public GameObject enemyHpBar;
}

public class EnemyDataBase : ScriptableObject
{
    public List<EnemyData> enemyList = new List<EnemyData>();

    public EnemyData GetEnemyID(int _id)
    {
        return enemyList.Find(enemy => enemy.enemyID == _id);
    }
}
