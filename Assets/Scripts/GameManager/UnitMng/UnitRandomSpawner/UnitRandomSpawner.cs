using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public interface IUnitRandomSpawner
{
    void randSpawn(int index);
}

public class UnitRandomSpawner : MonoBehaviour, IUnitRandomSpawner
{
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;
    public UnitMng unitMng;
    public IUnitMng iUnitMng;

    public bool randomDelay;
    public Image[] randomUnitImg;
    public Sprite[] randomStar;
    public float fadeTime;
    public int spawnCoin0;
    public int spawnCoin1;
    public int spawnCoin2;

    private void Awake()
    {
        iGoldCoin = goldCoin;
        iUnitMng = unitMng;

        randomDelay = false;
        fadeTime = 1;
        spawnCoin0 = 1;
        spawnCoin1 = 1;
        spawnCoin2 = 2;
    }

    public void initialized(IGoldCoin iGoldCoin, IUnitMng iUnitMng)
    {
        this.iGoldCoin = iGoldCoin;
        this.iUnitMng = iUnitMng;
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

    public bool canSpawn(int index)
    {
        if (index == 0)
        {
            return spawnCoin0 <= iGoldCoin.getCoin();
        }
        if (index == 1)
        {
            return spawnCoin1 <= iGoldCoin.getCoin();
        }
        if (index == 2)
        {
            return spawnCoin2 <= iGoldCoin.getCoin();
        }
        return false;
    }

    IEnumerator RandSpawn(int index)
    {
        randomDelay = true;
        int per = Random.Range(0, 10);
        int randB = Random.Range(0, iUnitMng.getUnitList(EUnitGrade.B).Count);
        int randA = Random.Range(0, iUnitMng.getUnitList(EUnitGrade.A).Count);
        int randS = Random.Range(0, iUnitMng.getUnitList(EUnitGrade.S).Count);

        Sprite randomSprite = GetRandomSprite(index, randB, randA, randS);

        switch (index)
        {
            case 0:
                if (per < 6)
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[0], randomSprite));
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[0], randomStar[3]));
                }
                break;
            case 1:
                if (per < 2)
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[1], randomSprite));
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[1], randomStar[3]));
                }
                break;
            case 2:
                if (per < 1)
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[2], randomSprite));
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[2], randomStar[3]));
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
                iGoldCoin.setCoin(-1);
                return iUnitMng.getUnitList(EUnitGrade.B)[randB].GetComponent<Unit>().UnitImg;

            case 1:
                iGoldCoin.setCoin(-1);
                return iUnitMng.getUnitList(EUnitGrade.A)[randA].GetComponent<Unit>().UnitImg;

            case 2:
                iGoldCoin.setCoin(-2);
                return iUnitMng.getUnitList(EUnitGrade.S)[randS].GetComponent<Unit>().UnitImg;
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
                    GameObject spawnUnitB = Instantiate(iUnitMng.getUnitList(EUnitGrade.B)[randB],
                        unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg[0].sprite = randomStar[0];
                    curUnitList.Add(spawnUnitB.GetComponent<Unit>());
                }
                else if (per >= 6)
                {
                    randomUnitImg[0].sprite = randomStar[0];
                }
                break;
            case 1:
                if (per < 2)
                {
                    GameObject spawnUnitA = Instantiate(iUnitMng.getUnitList(EUnitGrade.A)[randA],
                        unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg[1].sprite = randomStar[1];
                    curUnitList.Add(spawnUnitA.GetComponent<Unit>());
                }
                else if (per >= 2)
                {
                    randomUnitImg[1].sprite = randomStar[1];
                }
                break;
            case 2:
                if (per < 1)
                {
                    GameObject spawnUnitS = Instantiate(iUnitMng.getUnitList(EUnitGrade.S)[randS],
                        unitSpawnPointList[groundNum].transform.position,
                        Quaternion.identity, unitSpawnPointList[groundNum].transform);
                    randomUnitImg[2].sprite = randomStar[2];
                    curUnitList.Add(spawnUnitS.GetComponent<Unit>());
                }
                else if (per >= 1)
                {
                    randomUnitImg[2].sprite = randomStar[2];
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
