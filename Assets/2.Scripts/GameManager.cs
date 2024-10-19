using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Unity.Mathematics;

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
    { get { return coin; } set { coin = value; } }
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
    public float RewardGold 
    { get { return rewardGold; } set { rewardGold = value; } }
    public float RewardGem
    { get { return rewardGem; } set { rewardGem = value; } }
    public float RewardPaper
    { get { return rewardPaper; } set { rewardPaper = value; } }
    public float RewardExp
    { get { return rewardExp; } set { rewardExp = value; } }
    public float[][] FirstSelectWeight
    { get { return firstSelectWeight; } set { firstSelectWeight = value; } }

    public static GameManager Instance;

    [Header("시스템")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] int maxMonster;
    [SerializeField] float gold;
    [SerializeField] Image randomUnitImg1;
    [SerializeField] Image randomUnitImg2;
    [SerializeField] Image randomUnitImg3;
    [SerializeField] Sprite[] randomStar;
    float fadeTime = 1f;
    public List<Unit> curUnitList = new List<Unit>();
    int min;
    float sec;
    bool spawnTime;
    int curRound;
    int curMonster;
    float rewardGold;
    float rewardGem;
    float rewardPaper;
    float rewardExp;
    bool checkGameOver;
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
    bool randomDelay;

    [Header("유닛")]
    [SerializeField] List<GameObject> unitSpawnPointList = new List<GameObject>();
    string[] firstSelectOption = { "S","A", "B", "C"};
    [SerializeField] float[][] firstSelectWeight = new float[][]
    {
        new float[] { 0.03f, 0.10f, 0.15f, 0.72f },
        new float[] { 0.05f, 0.12f, 0.18f, 0.65f },
        new float[] { 0.07f, 0.14f, 0.21f, 0.58f },
        new float[] { 0.09f, 0.16f, 0.24f, 0.51f },
        new float[] { 0.11f, 0.18f, 0.27f, 0.44f },
        new float[] { 0.13f, 0.20f, 0.30f, 0.37f }
    };
    [SerializeField] List<GameObject> unitListSS = new List<GameObject>();
    [SerializeField] List<GameObject> unitListS = new List<GameObject>();
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
        maxMonster = 70;
        rewardGold = 0;
        rewardGem = 0;
        rewardPaper = 0;
        checkGameOver = false;
        gold = 10000f;
        spawnGold = 20;
        coin = 50;
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
        gameOver();
    }

    public void spawnUnit()
    {
        gold -= spawnGold;
        spawnGold += 2;

        string firstSelection = FirstSelectRandom(firstSelectOption, firstSelectWeight[(int)upgradeLevel4 - 1]);

        int randS = Random.Range(0, unitListS.Count);
        int randA = Random.Range(0, unitListA.Count);
        int randB = Random.Range(0, unitListB.Count);
        int randC = Random.Range(0, unitListC.Count);

        switch ( firstSelection ) 
        {
            case "S":
                GameObject spawnUnitS = Instantiate(unitListS[randS],
                    unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                curUnitList.Add(spawnUnitS.GetComponent<Unit>());
                break;
            case "A":
                GameObject spawnUnitA = Instantiate(unitListA[randA],
                    unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                curUnitList.Add(spawnUnitA.GetComponent<Unit>());
                break;
            case "B":
                GameObject spawnUnitB = Instantiate(unitListB[randB],
                    unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                curUnitList.Add(spawnUnitB.GetComponent<Unit>());
                break;
            case "C":
                GameObject spawnUnitC = Instantiate(unitListC[randC],
                    unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                curUnitList.Add(spawnUnitC.GetComponent<Unit>());
                break;
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

    public void randSpawn(int index)
    {
        if (randomDelay == false)
        {
            StartCoroutine(RandSpawn(index));
        }
        else
        {
            return;
        }
    }

    IEnumerator RandSpawn(int index)
    {
        randomDelay = true;
        int per = Random.Range(0, 10);
        int randB = Random.Range(0, unitListB.Count);
        int randA = Random.Range(0, unitListA.Count);
        int randS = Random.Range(0, unitListS.Count);

        Sprite randomSprite = GetRandomSprite(index, randB, randA, randS);

        switch (index) 
        {
            case 0:
                if (per < 6)
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg1, randomSprite));
                }
                else 
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg1, randomStar[3]));
                }
                break;
            case 1:
                if (per < 2)
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg2, randomSprite));
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg2, randomStar[3]));
                }
                break;
            case 2:
                if (per < 1)
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg3, randomSprite));
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg3, randomStar[3]));
                }
                break;
        }
        yield return new WaitForSeconds(1f);

        instantiateUnit(index, per, randB, randA, randS);

        randomDelay = false;
    }

    Sprite GetRandomSprite(int index, int randB, int randA, int randS)
    {
        switch (index) 
        {
            case 0:
                coin -= 1;
                return unitListB[randB].GetComponent<SpriteRenderer>().sprite;

            case 1:
                coin -= 1;
                return unitListA[randA].GetComponent<SpriteRenderer>().sprite;

            case 2:
                coin -= 2;
                return unitListS[randS].GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }

    private void instantiateUnit(int index, int per, int randB, int randA, int randS)
    {
        switch (index) 
        {
            case 0:
                if (per < 6)
                {
                    Instantiate(unitListB[randB], unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg1.sprite = randomStar[0];
                }
                else if (per >= 6)
                {
                    randomUnitImg1.sprite = randomStar[0];
                }
                break;
            case 1:
                if (per < 2)
                {
                    Instantiate(unitListA[randA], unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg2.sprite = randomStar[1];
                }
                else if (per >= 2)
                {
                    randomUnitImg2.sprite = randomStar[1];
                }
                break;
            case 2:
                if (per < 1)
                {
                    Instantiate(unitListS[randS], unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg3.sprite = randomStar[2];
                }
                else if (per >= 1)
                {
                    randomUnitImg3.sprite = randomStar[2];
                }
                break;
        }
    }

    IEnumerator FadeInImage(Image image, Sprite sprite)
    {
        float delayTime = 0f;
        image.sprite = sprite;
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        while(delayTime < fadeTime) 
        {
            delayTime += Time.deltaTime;
            color.a = Mathf.Clamp01(delayTime / fadeTime);
            image.color = color;
            yield return null;
        }
        color.a = 1;
        image.color = color;
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
        gameOverPanel.SetActive(true);
        checkGameOver = true;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.5f);

        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }

    private void clearGame()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator ClearGame()
    {
        gameOverPanel.SetActive(true);
        checkGameOver = false;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.5f);

        gameOverPanel.SetActive(false);
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
