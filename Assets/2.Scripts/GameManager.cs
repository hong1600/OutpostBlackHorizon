using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("시스템")]
    [SerializeField] GameObject warningPanel;
    [SerializeField] GameObject GameOverPanel;
    int min;
    float sec;
    bool spawnTime;
    int curRound;
    int curMonster;
    int maxMonster;
    bool checkWarning = true;
    bool checkGameOver = false;
    float gold;
    int spawnGold;
    int coin;
    int unitCount;

    [Header("유닛")]
    [SerializeField] GameObject unit;
    [SerializeField] List<GameObject> unitSpawnPointList = new List<GameObject>();

    [Header("몬스터")]
    [SerializeField] GameObject monster;
    [SerializeField] List<GameObject> enemy = new List<GameObject>();
    [SerializeField] GameObject enemySpawnPoint;
    [SerializeField] float enemySpawndelay = 0.85f;

    public int CurRound 
    { get { return curRound; } }
    public int Min 
    { get { return min; } }
    public float Sec 
    { get { return sec; } }
    public int CurMonster
    { get { return curMonster; } }
    public int MaxMonster
    { get { return maxMonster; } }
    public float MyGold
    { get { return gold; } }
    public int SpawnGold
    { get { return spawnGold; } }
    public int Coin
    { get { return coin; } }
    public int UnitCount
    { get { return unitCount; } }

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < unitSpawnPointList.Count; i++)
        {
            Instantiate(new GameObject($"spawnPoint{i}"), unit.transform);
        }

        enemySpawnPoint = GameObject.Find("SpawnPoint");

        min = 0;
        sec = 3;
        spawnTime = false;
        curRound = 0;
        curMonster = 0;
        maxMonster = 100;
        checkWarning = true;
        checkGameOver = false;
        gold = 170f;
        spawnGold = 20;
        coin = 0;
        unitCount = 0;
    }

    private void Update()
    {
        if (checkGameOver) return;

        countDown();
        spawnMonster();
        monsterCount();
        warning();
        gameOver();
    }

    public void spawnUnit()
    {
        gold -= spawnGold;
        spawnGold += 2;
    }

    public void spawnLevelUp()
    {

    }

    private void countDown()
    {
        sec -= Time.deltaTime;

        if(sec <= 0) 
        {
            StartCoroutine(spawn());

            if (min < 0)
            {
                min -= 1;
            }
        }
    }

    IEnumerator spawn()
    {
        curRound++;
        sec = 20f;
        spawnTime = true;

        yield return new WaitForSeconds(17);

        spawnTime = false;
    }

    private void spawnMonster()
    {
        if (enemySpawndelay <= 0 && spawnTime == true)
        {
            Instantiate(enemy[curRound], enemySpawnPoint.transform.position,
                Quaternion.identity, monster.transform);
            enemySpawndelay = 0.85f;
        }

        enemySpawndelay -= Time.deltaTime;
    }

    private void monsterCount()
    {
        curMonster = monster.transform.childCount;
    }

    private void warning()
    {
        if (curMonster >= maxMonster * 0.8f && checkWarning == true)
        {
            StartCoroutine(Warning());
            checkWarning = false;
        }
        if (curMonster < maxMonster * 0.8f)
        {
            checkWarning = true;
        }
    }

    IEnumerator Warning()
    {
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        warningPanel.SetActive(false);
    }

    private void gameOver()
    {
        if (curMonster >= maxMonster)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        GameOverPanel.SetActive(true);
        checkGameOver = true;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.5f);

        GameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
}
