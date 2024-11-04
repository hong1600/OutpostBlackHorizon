using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class EnemyMng : MonoBehaviour
{
    private GameManager gameManager;

    public Transform enemySpawnPoint;
    public GameObject enemyParent;
    public List<GameObject> enemy;
    public int curEnemyCount;
    public int maxEnemyCount;
    public float enemySpawndelay;

    public EnemyMng(GameManager manager)
    {
        gameManager = manager;
        maxEnemyCount = 100;
        enemySpawndelay = 0.85f;
    }

    public void spawnEnemy()
    {
        if (enemySpawndelay < 0 && GameManager.Instance.gameFlow.spawnTime == true 
            && !GameManager.Instance.gameFlow.bossRound)
        {
            Instantiate(enemy[GameManager.Instance.gameFlow.curRound], enemySpawnPoint.transform.position,
            Quaternion.identity, enemyParent.transform);
            enemySpawndelay = 0.85f;
        }
        if (GameManager.Instance.gameFlow.bossRound && enemySpawndelay < 0)
        {
            Instantiate(enemy[GameManager.Instance.gameFlow.curRound], enemySpawnPoint.transform.position,
            Quaternion.identity, enemyParent.transform);
            enemySpawndelay = 100f;
            GameManager.Instance.gameFlow.spawnTime = false;
        }
        enemySpawndelay -= Time.deltaTime;
    }

    public int enemyCount()
    {
        curEnemyCount = enemyParent.transform.childCount;
        return curEnemyCount;
    }
}
