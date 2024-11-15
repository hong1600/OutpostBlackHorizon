using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class UnitRandomSpawner : MonoBehaviour
{
    public IGameManager iGameManager;
    public IUnitMng iUnitMng;
    public UnitRandomSpawnerData data;

    public bool randomDelay;
    public Image randomUnitImg1;
    public Image randomUnitImg2;
    public Image randomUnitImg3;
    public Sprite[] randomStar;
    public float fadeTime;
    public int spawnCoin0;
    public int spawnCoin1;
    public int spawnCoin2;

    private void Awake()
    {
        data = Resources.Load<UnitRandomSpawnerData>("GameManager/UnitMngData/UnitRandomData/UnitRandomSpawnerData");

        randomDelay = false;
        randomUnitImg1 = data.randomUnitImg1;
        randomUnitImg2 = data.randomUnitImg2;
        randomUnitImg3 = data.randomUnitImg3;
        randomStar = data.randomStar;
        fadeTime = 1;
        spawnCoin0 = 1;
        spawnCoin1 = 1;
        spawnCoin2 = 2;
    }

    public void initialized(IGameManager iGameManager, UnitMng unitMng, IUnitMng iUnitMng)
    {
        this.iGameManager = iGameManager;
        this.iUnitMng = iUnitMng;
    }

    public bool canSpawn(int index)
    {
        if (index == 0)
        {
            return spawnCoin0 <= iGameManager.coinAmount();
        }
        if (index == 1)
        {
            return spawnCoin1 <= iGameManager.coinAmount();
        }
        if (index == 2)
        {
            return spawnCoin2 <= iGameManager.coinAmount();
        }
        return false;
    }

    public void randSpawn(int index)
    {
        if (randomDelay == false && canSpawn(index))
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
        int randB = Random.Range(0, iUnitMng.getUnitList(UnitType.B).Count);
        int randA = Random.Range(0, iUnitMng.getUnitList(UnitType.A).Count);
        int randS = Random.Range(0, iUnitMng.getUnitList(UnitType.S).Count);

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
                iGameManager.addCoin(-1);
                return iUnitMng.getUnitList(UnitType.B)[randB].GetComponent<SpriteRenderer>().sprite;

            case 1:
                iGameManager.addCoin(-1);
                return iUnitMng.getUnitList(UnitType.A)[randA].GetComponent<SpriteRenderer>().sprite;

            case 2:
                iGameManager.addCoin(-2);
                return iUnitMng.getUnitList(UnitType.S)[randS].GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }

    public void instantiateUnit(int index, int per, int randB, int randA, int randS)
    {
        List<GameObject> unitSpawnPointList = iUnitMng.getUnitSpawnPointList();
        int groundNum = iUnitMng.getGroundNum();
        List<Unit> curUnitList = iUnitMng.getCurUnitList();

        switch (index)
        {
            case 0:
                if (per < 6)
                {
                    GameObject spawnUnitB = Instantiate(iUnitMng.getUnitList(UnitType.B)[randB],
                        unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg1.sprite = randomStar[0];
                    curUnitList.Add(spawnUnitB.GetComponent<Unit>());
                }
                else if (per >= 6)
                {
                    randomUnitImg1.sprite = randomStar[0];
                }
                break;
            case 1:
                if (per < 2)
                {
                    GameObject spawnUnitA = Instantiate(iUnitMng.getUnitList(UnitType.A)[randA],
                        unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg2.sprite = randomStar[1];
                    curUnitList.Add(spawnUnitA.GetComponent<Unit>());
                }
                else if (per >= 2)
                {
                    randomUnitImg2.sprite = randomStar[1];
                }
                break;
            case 2:
                if (per < 1)
                {
                    GameObject spawnUnitS = Instantiate(iUnitMng.getUnitList(UnitType.S)[randS],
                        unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg3.sprite = randomStar[2];
                    curUnitList.Add(spawnUnitS.GetComponent<Unit>());
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

        while (delayTime < fadeTime)
        {
            delayTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, delayTime / fadeTime);
            image.color = color;
            yield return null;
        }
        color.a = 1;
        image.color = color;
    }
}
