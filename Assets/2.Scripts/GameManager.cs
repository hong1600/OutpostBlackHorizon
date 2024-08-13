using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int min;
    float sec;
    [SerializeField] bool spawnTime;
    [SerializeField]int curRound;
    [SerializeField]int curMonster;
    [SerializeField] int maxMonster;
    [SerializeField] GameObject warningPanel;
    [SerializeField] GameObject GameOverPanel;

    [Header("¿Ø¥÷")]
    [SerializeField] GameObject unit;
    [SerializeField] List<GameObject> unitSpawnPointList = new List<GameObject>();

    [Header("∏ÛΩ∫≈Õ")]
    [SerializeField] GameObject monster;
    [SerializeField] List<GameObject> enemy = new List<GameObject>();
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
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
        sec = 3;
        min = 0;
        curRound = 0;
        spawnTime = false;
        curMonster = 0;
        maxMonster = 100;
    }

    private void Update()
    {
        countDown();
        spawnMonster();
        monsterCounter();
    }

    private void countDown()
    {
        sec -= Time.deltaTime;

        if(sec <= 0) 
        {
            StartCoroutine(spawner());

            if (min < 0)
            {
                min -= 1;
            }
        }
    }

    IEnumerator spawner()
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

    private void monsterCounter()
    {
        curMonster = monster.transform.childCount;
    }

    private void warning()
    {
        if (curMonster >= maxMonster / curMonster * 80)
        {
            StartCoroutine(Warning());
        }
    }

    IEnumerator Warning()
    {
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        warningPanel.SetActive(false);
    }

    private void gameOver()
    {
        if (curMonster == maxMonster)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        GameOverPanel.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        GameOverPanel.SetActive(false);
        SceneManager.LoadScene(3);
    }
}
