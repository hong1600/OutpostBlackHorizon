using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class GameFlow : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject waveBoss;
    public int waveBossLevel;
    public int curRound;
    public float wavebossDelay;
    public bool bossRound;
    public float min, sec;
    public bool spawnTime;

    public GameFlow(GameManager manager)
    {
        gameManager = manager;
        curRound = 0;
        wavebossDelay = 25f;
        bossRound = false;
    }

    public void timer()
    {
        if (bossRound)
        {
            sec = 0f;
        }
        else
        {
            sec -= Time.deltaTime;
        }
        int intsec = (int)sec;

        if (sec < 0f)
        {
            StartCoroutine(spawn());

            if (min > 0)
            {
                min -= 1;
            }
        }
    }

    IEnumerator spawn()
    {
        curRound++;
        roundCheck();
        sec = 20f;
        spawnTime = true;
        GameUI.instance.spawnPointTimerPanel.SetActive(false);

        yield return new WaitForSeconds(17);

        spawnTime = false;
    }

    public void spawnWaveBoss()
    {
        GameUI.instance.spawnWaveBossBtn.SetActive(false);

        Instantiate(waveBoss, GameManager.Instance.enemyMng.enemySpawnPoint.position,
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

    public void roundCheck()
    {
        if (curRound == 5 || curRound == 10)
        {
            bossRound = true;
        }
        else
        {
            bossRound = false;
        }
    }
}
