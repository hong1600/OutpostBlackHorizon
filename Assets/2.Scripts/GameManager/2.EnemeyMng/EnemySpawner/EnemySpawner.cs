using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyMng enemyMng;

    public Transform enemySpawnPoint;
    public List<GameObject> enemy;
    public float enemySpawndelay;

    public void initialize(EnemyMng manager)
    {
        enemyMng = manager;
    }

    public void spawnEnemy()
    {
        if (enemySpawndelay < 0 && GameManager.Instance.gameFlow.roundTimer.spawnTime == true
            && !GameManager.Instance.gameFlow.roundTimer.bossRound)
        {
            Instantiate(enemy[GameManager.Instance.gameFlow.roundTimer.curRound], enemySpawnPoint.transform.position,
            Quaternion.identity, enemyMng.enemyParent.transform);
            enemySpawndelay = 0.85f;
        }
        if (GameManager.Instance.gameFlow.roundTimer.bossRound && enemySpawndelay < 0)
        {
            Instantiate(enemy[GameManager.Instance.gameFlow.roundTimer.curRound], enemySpawnPoint.transform.position,
            Quaternion.identity, enemyMng.enemyParent.transform);
            enemySpawndelay = 100f;
            GameManager.Instance.gameFlow.roundTimer.spawnTime = false;
        }
        enemySpawndelay -= Time.deltaTime;
    }

}
