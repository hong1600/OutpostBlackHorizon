using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBossSpawner : MonoBehaviour
{
    public EnemyMng enemyMng;

    public GameObject waveBoss;
    public int waveBossLevel;
    public int curRound;
    public float wavebossDelay;

    public void initialized(EnemyMng manager)
    {
        enemyMng = manager;   
    }

    public void spawnWaveBoss()
    {
        GameUI.instance.spawnWaveBossBtn.SetActive(false);

        Instantiate(waveBoss, GameManager.Instance.enemyMng.enemySpawner.enemySpawnPoint.position,
                Quaternion.identity, GameManager.Instance.enemyMng.enemyParent.transform);
        wavebossDelay = 1000f;
    }
    public void spawnWaveBossTime()
    {
        if (GameUI.instance.spawnWaveBossBtn.activeSelf == false)
        {
            wavebossDelay -= Time.deltaTime;

            if (wavebossDelay < 0)
            {
                GameUI.instance.spawnWaveBossBtn.SetActive(true);
            }
        }
    }

}
