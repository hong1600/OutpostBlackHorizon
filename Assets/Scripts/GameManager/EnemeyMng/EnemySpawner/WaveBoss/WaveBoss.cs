using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaveBoss 
{
    int getWaveBossLevel();
    void setWaveBossDelay(float value);
    void spawnWaveBoss();
}

public class WaveBoss : MonoBehaviour, IWaveBoss
{
    public EnemySpawner enemySpawner;
    public IEnemySpawner iEnemySpawner;
    public EnemyMng enemyMng;
    public IEnemyMng iEnemyMng;

    public GameObject waveBoss;
    public int waveBossLevel;
    public float wavebossDelay;

    private void Awake()
    {
        iEnemySpawner = enemySpawner;
        iEnemyMng = enemyMng;
        waveBossLevel = 1;
        wavebossDelay = 25f;
    }

    public  void spawnWaveBoss()
    {
        //bossUI.spawnWaveBossBtn.SetActive(false);

        Instantiate(waveBoss, iEnemySpawner.getEnemySpawnPoint().position,
        Quaternion.identity, iEnemyMng.getEnemyParent().transform);
        wavebossDelay = 1000f;
    }

    //public void spawnWaveBossTime()
    //{
    //    if (bossUI.spawnWaveBossBtn.activeSelf == false)
    //    {
    //        wavebossDelay -= Time.deltaTime;

    //        if (wavebossDelay < 0)
    //        {
    //            bossUI.spawnWaveBossBtn.SetActive(true);
    //        }
    //    }
    //}

    public int getWaveBossLevel() { return waveBossLevel; }
    public void setWaveBossDelay(float value) { wavebossDelay = value; }
}
