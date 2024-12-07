using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawner
{
    void initEnemy(Enemy enemy);
    void spawnEnemy();
    float getEnemySpawnDelay();
    void setEnemySpawnDelay(float value);
    Transform getEnemySpawnPoint();
    Transform[] getWayPoint();
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    public EnemyMng enemyMng;
    public IEnemyMng iEnemyMng;
    public Timer timer;
    public ISpawnTime iSpawnTime;
    public Round round;
    public IRound iRound;
    public Rewarder rewarder;
    public GoldCoin goldCoin;
    public WaveBossSpawner waveBossSpawner;
    public BossSpawner bossSpawner;

    public List<GameObject> enemyList;
    public Transform enemySpawnPoint;
    public Transform[] wayPoint;
    public float enemySpawnDelay;
    public int curEnemy;

    private void Awake()
    {
        iSpawnTime = timer;
        iRound = round;
        iEnemyMng = enemyMng;
        curEnemy = 0;
        enemySpawnDelay = 0;
    }

    public void spawnEnemy()
    {
        if (iRound.getCurRound() == 0 || iRound.isBossRound()) { return; }

        if (enemySpawnDelay <= 0)
        {
            GameObject obj = Instantiate(enemyList[curEnemy], enemySpawnPoint.transform.position,
            Quaternion.identity, iEnemyMng.getEnemyParent().transform);

            Enemy enemy = obj.GetComponent<Enemy>();

            initEnemy(enemy);

            enemySpawnDelay = 0.85f;
        }
        enemySpawnDelay -= Time.deltaTime;
    }

    public void initEnemy(Enemy enemy)
    {
        enemy.iEnemySpawner = this;
        enemy.iRewarder = this.rewarder;
        enemy.iGoldCoin = this.goldCoin;
        enemy.iRound = this.round;
        enemy.iTimer = this.timer;
        enemy.iWaveBossSpawner = this.waveBossSpawner;
        enemy.iBossSpawner = this.bossSpawner;
    }

    public float getEnemySpawnDelay() {  return enemySpawnDelay; }
    public void setEnemySpawnDelay(float value) { enemySpawnDelay = value; }
    public Transform getEnemySpawnPoint() { return enemySpawnPoint; }
    public Transform[] getWayPoint() { return wayPoint; }
}
