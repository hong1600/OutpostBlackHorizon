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

    public GameObject randomUnit;
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
        for (int i = 0; i < iUnitMng.getUnitSpawnPointList().Count; i++)
        {
            if (iUnitMng.getUnitSpawnPointList()[i].transform.childCount == 0)
            {
                switch (index)
                {
                    case 0:
                        return spawnCoin0 <= iGoldCoin.getCoin();

                    case 1:
                        return spawnCoin1 <= iGoldCoin.getCoin();

                    case 2:
                        return spawnCoin2 <= iGoldCoin.getCoin();

                    default:
                        return false;
                }
            }
        }
        return false;
    }

    IEnumerator RandSpawn(int index)
    {
        randomDelay = true;
        int per = Random.Range(0, 10);
        int randB = Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.B).Count);
        int randA = Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.A).Count);
        int randS = Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.S).Count);

        Sprite randomSprite = GetRandomSprite(index, randB, randA, randS);

        iUnitMng.checkGround(randomUnit);

        switch (index)
        {
            case 0:
                if (per < 6)
                {
                    randomUnit = iUnitMng.getUnitByGradeList(EUnitGrade.B)[randB];
                    yield return StartCoroutine(FadeInImage(randomUnitImg[0], randomSprite));
                    iUnitMng.unitInstantiate(randomUnit);
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[0], randomStar[3]));
                }
                break;
            case 1:
                if (per < 2)
                {
                    randomUnit = iUnitMng.getUnitByGradeList(EUnitGrade.A)[randA];
                    yield return StartCoroutine(FadeInImage(randomUnitImg[1], randomSprite));
                    iUnitMng.unitInstantiate(randomUnit);
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[1], randomStar[3]));
                }
                break;
            case 2:
                if (per < 1)
                {
                    randomUnit = iUnitMng.getUnitByGradeList(EUnitGrade.S)[randS];
                    yield return StartCoroutine(FadeInImage(randomUnitImg[2], randomSprite));
                    iUnitMng.unitInstantiate(randomUnit);
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImg[2], randomStar[3]));
                }
                break;
        }

        yield return new WaitForSeconds(1f);

        randomDelay = false;
    }

    Sprite GetRandomSprite(int index, int randB, int randA, int randS)
    {
        switch (index)
        {
            case 0:
                return iUnitMng.getUnitByGradeList(EUnitGrade.B)[randB].GetComponent<Unit>().UnitImg;

            case 1:
                return iUnitMng.getUnitByGradeList(EUnitGrade.A)[randA].GetComponent<Unit>().UnitImg;

            case 2:
                return iUnitMng.getUnitByGradeList(EUnitGrade.S)[randS].GetComponent<Unit>().UnitImg;
        }
        return null;
    }


    IEnumerator FadeInImage(Image image, Sprite sprite)
    {
        float delayTime = 0f;
        Sprite origin = image.sprite;
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
        image.sprite = origin;

    }
}
