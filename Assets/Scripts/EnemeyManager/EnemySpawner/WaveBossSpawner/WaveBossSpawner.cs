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

    public void SpawnWaveBoss()
    {
        waveBossPanel.SetActive(false);

        int rand = Random.Range(0, Shared.enemyManager.iEnemySpawner.GetEnemySpawnPointList().Count);
        GameObject obj = Instantiate(waveBoss, Shared.enemyManager.iEnemySpawner.GetEnemySpawnPointList()[rand].position,
        Quaternion.identity, Shared.enemyManager.iEnemyMng.GetEnemyParent()[4].transform);

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
