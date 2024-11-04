using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[System.Serializable]
public class UnitMng : MonoBehaviour
{
    private GameManager gameManager;

    public List<Unit> curUnitList = new List<Unit>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
    public List<UnitData> needUnitList = new List<UnitData>();
    public List<Unit> unitToMix = new List<Unit>();
    public List<GameObject> unitListSS, unitListS, unitListA, unitListB, unitListC;
    public int spawnGold;
    public int coin;
    public float[][] firstSelectWeight = new float[][]
{
        new float[] { 0.03f, 0.10f, 0.15f, 0.72f },
        new float[] { 0.05f, 0.12f, 0.18f, 0.65f },
        new float[] { 0.07f, 0.14f, 0.21f, 0.58f },
        new float[] { 0.09f, 0.16f, 0.24f, 0.51f },
        new float[] { 0.11f, 0.18f, 0.27f, 0.44f },
        new float[] { 0.13f, 0.20f, 0.30f, 0.37f }
};
    public string[] firstSelectOption = { "S", "A", "B", "C" };
    public int groundNum;
    public bool randomDelay;
    public Image randomUnitImg1;
    public Image randomUnitImg2;
    public Image randomUnitImg3;
    public Sprite[] randomStar;
    public float fadeTime = 1f;

    public UnitMng(GameManager manager)
    {
        gameManager = manager;
    }

    public void spawnUnit()
    {
        if (!checkGround()) return;

        GameManager.Instance.myGold -= spawnGold;
        spawnGold += 2;

        string firstSelection = FirstSelectRandom(firstSelectOption, 
            firstSelectWeight[(int)GameManager.Instance.upgradeMng.upgradeLevel4 - 1]);

        int randS = Random.Range(0, unitListS.Count);
        int randA = Random.Range(0, unitListA.Count);
        int randB = Random.Range(0, unitListB.Count);
        int randC = Random.Range(0, unitListC.Count);

        switch (firstSelection)
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

    public string FirstSelectRandom(string[] options, float[] weights)
    {
        float totalWeight = 0f;

        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < options.Length; i++)
        {
            cumulativeWeight += weights[i];

            if (randomValue < cumulativeWeight)
            {
                return options[i];
            }
        }

        return options[options.Length - 1];
    }

    public bool checkGround()
    {
        for (groundNum = 0; groundNum < unitSpawnPointList.Count; groundNum++)
        {
            if (unitSpawnPointList[groundNum].transform.childCount <= 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool canMixUnit()
    {
        unitToMix.Clear();

        foreach (Unit fieldUnit in curUnitList)
        {
            foreach (UnitData needUnit in GameInventoryManager.Instance.needUnitList)
            {
                if (fieldUnit.unitName == needUnit.unitName &&
                    !unitToMix.Any(unit => unit.unitName == fieldUnit.unitName))
                {
                    unitToMix.Add(fieldUnit);
                }
            }
        }

        return unitToMix.Count == needUnitList.Count;
    }

    public void MixUnitSpawn()
    {
        if (canMixUnit())
        {
            GameObject spawnUnit = Instantiate(unitListSS[GameInventoryManager.Instance.curMixUnit],
                unitSpawnPointList[groundNum].transform.position, Quaternion.identity,
                unitSpawnPointList[groundNum].transform);
            curUnitList.Add(spawnUnit.GetComponent<Unit>());
            GameUI.instance.mixPanel.SetActive(false);
        }
        else return;
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

    public void instantiateUnit(int index, int per, int randB, int randA, int randS)
    {
        switch (index)
        {
            case 0:
                if (per < 6)
                {
                    GameObject spawnUnitB = Instantiate(unitListB[randB], unitSpawnPointList[groundNum].transform.position,
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
                    GameObject spawnUnitA = Instantiate(unitListA[randA], unitSpawnPointList[groundNum].transform.position,
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
                    GameObject spawnUnitS = Instantiate(unitListS[randS], unitSpawnPointList[groundNum].transform.position,
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
            color.a = Mathf.Clamp01(delayTime / fadeTime);
            image.color = color;
            yield return null;
        }
        color.a = 1;
        image.color = color;
    }
}
