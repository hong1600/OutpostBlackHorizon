using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public RoundTimer roundTimer;
    public EnemyMng enemyMng;
    public GameStateCheck gameStateCheck;

    public Transform enemySpawnPoint;
    public List<GameObject> enemy;
    public float enemySpawndelay;
    public bool canSpawn;

    private void Update()
    {
        if (canSpawn)
        {
            spawnEnemy();
        }

    }

    public void spawnEnemy()
    {
        if (enemySpawndelay < 0 && roundTimer.spawnTime == true
            && !roundTimer.bossRound)
        {
            Instantiate(enemy[roundTimer.curRound], enemySpawnPoint.transform.position,
            Quaternion.identity, enemyMng.enemyParent.transform);
            enemySpawndelay = 0.85f;
        }
        if (roundTimer.bossRound && enemySpawndelay < 0)
        {
            Instantiate(enemy[roundTimer.curRound], enemySpawnPoint.transform.position,
            Quaternion.identity, enemyMng.enemyParent.transform);
            enemySpawndelay = 100f;
            roundTimer.spawnTime = false;
        }
        enemySpawndelay -= Time.deltaTime;
    }

}
