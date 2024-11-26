using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaveBoss 
{
    int getWaveBossLevel();
    void setWaveBossDelay(float value);
    void spawnWaveBoss();
}

public class WaveBoss : EnemySpawner, IWaveBoss
{
    public GameObject waveBoss;
    public GameObject waveBossBtn;
    public int waveBossLevel;
    public float wavebossDelay;

    private void Awake()
    {
        waveBossLevel = 1;
        wavebossDelay = 25f;
    }

    public  void spawnWaveBoss()
    {
        waveBossBtn.SetActive(false);

        Instantiate(waveBoss, enemySpawnPoint.position,
        Quaternion.identity, iEnemyMng.getEnemyParent().transform);
        wavebossDelay = 1000f;
    }

    public void spawnWaveBossTime()
    {
        if (waveBossBtn.activeSelf == false)
        {
            wavebossDelay -= Time.deltaTime;

            if (wavebossDelay < 0)
            {
                waveBossBtn.SetActive(true);
            }
        }
    }

    public int getWaveBossLevel() { return waveBossLevel; }
    public void setWaveBossDelay(float value) { wavebossDelay = value; }
}
