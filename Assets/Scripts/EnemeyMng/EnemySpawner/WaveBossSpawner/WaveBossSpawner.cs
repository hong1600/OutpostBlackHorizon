using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaveBossSpawner
{
    int GetWaveBossLevel();
    void SetWaveBossDelay(float _value);
    void SpawnWaveBoss();
    void SpawnWaveBossTime();
}

public class WaveBossSpawner : MonoBehaviour, IWaveBossSpawner
{
    public GameObject waveBoss;
    public GameObject waveBossBtn;
    public GameObject waveBossPanel;
    public int waveBossLevel;
    public float wavebossDelay;

    private void Awake()
    {
        waveBossLevel = 1;
        wavebossDelay = 25f;
    }

    public void SpawnWaveBoss()
    {
        waveBossPanel.SetActive(false);

        int rand = Random.Range(0, Shared.enemyMng.iEnemySpawner.GetEnemySpawnPoints().Length);
        GameObject obj = Instantiate(waveBoss, Shared.enemyMng.iEnemySpawner.GetEnemySpawnPoints()[rand].position,
        Quaternion.identity, Shared.enemyMng.enemyParent.transform);

        Enemy enemy = obj.GetComponent<Enemy>();

        wavebossDelay = 25f;
    }

    public void SpawnWaveBossTime()
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

    public int GetWaveBossLevel() { return waveBossLevel; }
    public void SetWaveBossDelay(float _value) { wavebossDelay = _value; }
}
