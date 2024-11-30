using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMng 
{
    int enemyCount();
    int getMaxEnemyCount();
    GameObject getEnemyParent();
}
public class EnemyMng : MonoBehaviour, IEnemyMng
{
    public GameObject enemyParent;
    public int maxEnemyCount;
    public int curEnemyCount;

    private void Awake()
    {
        maxEnemyCount = 100;
        curEnemyCount = 0;
    }

    public int enemyCount()
    {
        curEnemyCount = enemyParent.transform.childCount;
        return curEnemyCount;
    }

    public int getMaxEnemyCount() { return maxEnemyCount; }

    public GameObject getEnemyParent() { return enemyParent; }
}
