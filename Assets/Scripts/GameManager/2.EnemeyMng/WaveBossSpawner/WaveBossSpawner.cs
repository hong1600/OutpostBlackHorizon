using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBossSpawner : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public BossUI bossUI;
    public EnemyMng enemyMng;

    public GameObject waveBoss;
    public int waveBossLevel;
    public float wavebossDelay;

    private void Awake()
    {
        waveBossLevel = 1;
        wavebossDelay = 25f;
    }

    public void spawnWaveBoss()
    {
        bossUI.spawnWaveBossBtn.SetActive(false);

        Instantiate(waveBoss, enemySpawner.enemySpawnPoint.position,
                Quaternion.identity, enemyMng.enemyParent.transform);
        wavebossDelay = 1000f;
    }
    public void spawnWaveBossTime()
    {
        if (bossUI.spawnWaveBossBtn.activeSelf == false)
        {
            wavebossDelay -= Time.deltaTime;

            if (wavebossDelay < 0)
            {
                bossUI.spawnWaveBossBtn.SetActive(true);
            }
        }
    }

}
