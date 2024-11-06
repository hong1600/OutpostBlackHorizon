using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;


public class UnitRandomSpawner : MonoBehaviour
{
    public UnitMng unitMng;

    public bool randomDelay;
    public Image randomUnitImg1;
    public Image randomUnitImg2;
    public Image randomUnitImg3;
    public Sprite[] randomStar;
    public float fadeTime;
    public int spawnCoin;

    public void initialize(UnitMng manager)
    {
        unitMng = manager;
    }

    public bool canSpawn()
    {
        return spawnCoin <= GameManager.Instance.myCoin;
    }

    public void randSpawn(int index)
    {
        if (randomDelay == false && canSpawn())
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
        int randB = Random.Range(0, unitMng.unitListB.Count);
        int randA = Random.Range(0, unitMng.unitListA.Count);
        int randS = Random.Range(0, unitMng.unitListS.Count);

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
                GameManager.Instance.myCoin -= 1;
                return unitMng.unitListB[randB].GetComponent<SpriteRenderer>().sprite;

            case 1:
                GameManager.Instance.myCoin -= 1;
                return unitMng.unitListA[randA].GetComponent<SpriteRenderer>().sprite;

            case 2:
                GameManager.Instance.myCoin -= 2;
                return unitMng.unitListS[randS].GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }

    public void instantiateUnit(int index, int per, int randB, int randA, int randS)
    {
        switch (index)
        {
            case 0:
                if (per < 6)
                {
                    GameObject spawnUnitB = Instantiate(unitMng.unitListB[randB],
                        unitMng.unitSpawnPointList[unitMng.groundNum].transform.position,
                        Quaternion.identity, unitMng.unitSpawnPointList[unitMng.groundNum].transform);
                    randomUnitImg1.sprite = randomStar[0];
                    unitMng.curUnitList.Add(spawnUnitB.GetComponent<Unit>());
                }
                else if (per >= 6)
                {
                    randomUnitImg1.sprite = randomStar[0];
                }
                break;
            case 1:
                if (per < 2)
                {
                    GameObject spawnUnitA = Instantiate(unitMng.unitListA[randA],
                        unitMng.unitSpawnPointList[unitMng.groundNum].transform.position,
                        Quaternion.identity, unitMng.unitSpawnPointList[unitMng.groundNum].transform);
                    randomUnitImg2.sprite = randomStar[1];
                    unitMng.curUnitList.Add(spawnUnitA.GetComponent<Unit>());
                }
                else if (per >= 2)
                {
                    randomUnitImg2.sprite = randomStar[1];
                }
                break;
            case 2:
                if (per < 1)
                {
                    GameObject spawnUnitS = Instantiate(unitMng.unitListS[randS],
                        unitMng.unitSpawnPointList[unitMng.groundNum].transform.position,
                        Quaternion.identity, unitMng.unitSpawnPointList[unitMng.groundNum].transform);
                    randomUnitImg3.sprite = randomStar[2];
                    unitMng.curUnitList.Add(spawnUnitS.GetComponent<Unit>());
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
            color.a = Mathf.Clamp01(delayTime / fadeTime);
            image.color = color;
            yield return null;
        }
        color.a = 1;
        image.color = color;
    }
}
