using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaveBossSpawner
{
    int getWaveBossLevel();
    void setWaveBossDelay(float value);
    void spawnWaveBoss();
    void spawnWaveBossTime();
}

public class WaveBossSpawner : MonoBehaviour, IWaveBossSpawner
{
    public EnemySpawner enemySpawner;
    public IEnemySpawner iEnemySpawner;
    public EnemyMng enemyMng;
    public IEnemyMng iEnemyMng;

    public GameObject waveBoss;
    public GameObject waveBossBtn;
    public GameObject waveBossPanel;
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
        waveBossPanel.SetActive(false);

        Instantiate(waveBoss, iEnemySpawner.getEnemySpawnPoint().position,
        Quaternion.identity, iEnemyMng.getEnemyParent().transform);
        wavebossDelay = 25f;
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
