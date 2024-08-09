using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject spawnPoint;
    float spawnTime = 1;

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
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    private void Update()
    {
        spawnMonster();
    }

    private void spawnMonster()
    {
        if (spawnTime <= 0)
        {
            Instantiate(enemy1, spawnPoint.transform.position, Quaternion.identity);
            spawnTime = 0.3f;
        }

        spawnTime -= Time.deltaTime;
    }

}
