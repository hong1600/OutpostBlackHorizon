using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float spawndelay = 0.4f;

    int min;
    float sec;
    [SerializeField] bool spawnTime;
    [SerializeField] int curRound;
    [SerializeField] int curMonster;
    [SerializeField] int maxMonster;


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
        sec = 3;
        min = 0;
        curRound = 0;
        spawnTime = false;
        spawnPoint = GameObject.Find("SpawnPoint");
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
        if (spawndelay <= 0 && spawnTime == true)
        {
            Instantiate(enemy1, spawnPoint.transform.position, Quaternion.identity);
            spawndelay = 0.4f;
        }

        spawndelay -= Time.deltaTime;
    }

    private void monsterCounter()
    {
        
    }
}
