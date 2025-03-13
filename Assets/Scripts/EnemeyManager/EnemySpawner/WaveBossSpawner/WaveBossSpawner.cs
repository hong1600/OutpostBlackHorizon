using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBossSpawner : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemySpawner enemySpawner;

    [SerializeField] GameObject waveBoss;
    [SerializeField] GameObject waveBossBtn;
    [SerializeField] GameObject waveBossPanel;
    [SerializeField] int waveBossLevel;
    [SerializeField] float wavebossDelay;
    [SerializeField] Transform waveBossParent;

    private void Awake()
    {
        waveBossLevel = 1;
        wavebossDelay = 25f;
    }

    private void Start()
    {
        enemyManager = Shared.enemyManager;
        enemySpawner = Shared.enemyManager.EnemySpawner;
    }

    public void SpawnWaveBoss()
    {
        waveBossPanel.SetActive(false);

        int rand = Random.Range(0, enemySpawner.GetEnemySpawnPointList().Count);
        GameObject obj = Instantiate(waveBoss, enemySpawner.GetEnemySpawnPointList()[rand].position,
        Quaternion.identity, enemyManager.GetEnemyParent()[4].transform);

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
