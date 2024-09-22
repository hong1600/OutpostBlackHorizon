using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    public float Gold
    { get { return gold; } set { gold = value; } }
    public int SpawnGold
    { get { return spawnGold; } }
    public int Coin
    { get { return coin; } }
    public int UnitCount
    { get { return groundNum; } }
    public float UpgradeCost1 
    { get { return upgradeCost1; } set { upgradeCost1 = value; } }
    public float UpgradeCost2
    { get { return upgradeCost2; } set { upgradeCost2 = value; } }
    public float UpgradeCost3
    { get { return upgradeCost3; } set { upgradeCost3 = value; } }
    public float UpgradeCost4
    { get { return upgradeCost4; } set { upgradeCost4 = value; } }
    public float UpgradeLevel1
    { get { return upgradeLevel1; } set { upgradeLevel1 = value; } }
    public float UpgradeLevel2
    { get { return upgradeLevel2; } set { upgradeLevel2 = value; } }
    public float UpgradeLevel3
    { get { return upgradeLevel3; } set { upgradeLevel3 = value; } }
    public float UpgradeLevel4
    { get { return upgradeLevel4; } set { upgradeLevel4 = value; } }

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
    [SerializeField] float gold;
    int spawnGold;
    int coin;
    float upgradeCost1;
    float upgradeCost2;
    float upgradeCost3;
    float upgradeCost4;
    float upgradeLevel1;
    float upgradeLevel2;
    float upgradeLevel3;
    float upgradeLevel4;

    [Header("유닛")]
    [SerializeField] GameObject unit;
    [SerializeField] List<GameObject> unitSpawnPointList = new List<GameObject>();
    string[] firstSelectOption = { "A", "B", "C" };
    float[] firstSelectWeight = { 0.2f, 0.3f, 0.5f };
    [SerializeField] List<GameObject> unitListA = new List<GameObject>();
    [SerializeField] List<GameObject> unitListB = new List<GameObject>();
    [SerializeField] List<GameObject> unitListC = new List<GameObject>();
    int groundNum;

    [Header("몬스터")]
    [SerializeField] GameObject monster;
    [SerializeField] List<GameObject> enemy = new List<GameObject>();
    [SerializeField] GameObject enemySpawnPoint;
    [SerializeField] float enemySpawndelay = 0.85f;



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
        enemySpawnPoint = GameObject.Find("SpawnPoint");

        min = 0;
        sec = 5;
        spawnTime = false;
        curRound = 0;
        curMonster = 0;
        maxMonster = 100;
        checkWarning = true;
        checkGameOver = false;
        gold = 170f;
        spawnGold = 20;
        coin = 0;
        upgradeCost1 = 30;
        upgradeCost2 = 50;
        upgradeCost3 = 1;
        upgradeCost4 = 100;
        upgradeLevel1 = 1;
        upgradeLevel2 = 1;
        upgradeLevel3 = 1;
        upgradeLevel4 = 1;
    }

    private void Update()
    {
        if (checkGameOver) return;

        CheckGround();
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

        string firstSelection = FirstSelectRandom(firstSelectOption, firstSelectWeight);

        int randA = Random.Range(0, unitListA.Count);
        int randB = Random.Range(0, unitListB.Count);
        int randC = Random.Range(0, unitListC.Count);

        switch ( firstSelection ) 
        {
            case "A":
                Instantiate(unitListA[randA], unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;
            case "B":
                Instantiate(unitListB[randB], unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;
            case "C":
                Instantiate(unitListC[randC], unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;
        }
    }

    private void CheckGround()  
    {
        for (groundNum = 0; groundNum < unitSpawnPointList.Count; groundNum++)
        {
            if (unitSpawnPointList[groundNum].transform.childCount <= 0)
            {
                return;
            }
        }
    }

    string FirstSelectRandom(string[] options, float[] weights)
    {
        float totalWeight = 0f;

        foreach(float weight in weights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        for(int i = 0; i < options.Length; i++) 
        {
            cumulativeWeight += weights[i];

            if(randomValue < cumulativeWeight) 
            {
                return options[i];
            }
        }

        return options[options.Length - 1];
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

    public void upgradeBtn(int index)
    {
        if (upgradeCost1 < gold && index == 1)
        {
            gold -= upgradeCost1;
            upgradeCost1 *= 2;
            upgradeLevel1++;
        }
    }
}
