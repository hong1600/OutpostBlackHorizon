using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public interface IUnitRandomSpawner
{
    void RandSpawn(int _index);
}

public class UnitRandomSpawner : MonoBehaviour, IUnitRandomSpawner
{
    public GameObject randomUnit;
    public bool randomDelay;
    public Sprite randomFail;
    public Image[] randomUnitImgs;
    public float fadeTime;
    public int spawnCoin0;
    public int spawnCoin1;
    public int spawnCoin2;

    private void Awake()
    {
        randomDelay = false;
        fadeTime = 1;
        spawnCoin0 = 1;
        spawnCoin1 = 1;
        spawnCoin2 = 2;
    }

    public void RandSpawn(int _index)
    {
        if (randomDelay == false && CanSpawn(_index))
        {
            StartCoroutine(RandSpawnSelect(_index));
        }
        else
        {
            return;
        }
    }

    public bool CanSpawn(int _index)
    {
        for (int i = 0; i < Shared.unitMng.GetUnitSpawnPointList().Count; i++)
        {
            if (Shared.unitMng.GetUnitSpawnPointList()[i].transform.childCount == 0)
            {
                switch (_index)
                {
                    case 0:
                        return spawnCoin0 <= Shared.gameMng.iGoldCoin.GetCoin();

                    case 1:
                        return spawnCoin1 <= Shared.gameMng.iGoldCoin.GetCoin();

                    case 2:
                        return spawnCoin2 <= Shared.gameMng.iGoldCoin.GetCoin();

                    default:
                        return false;
                }
            }
        }
        return false;
    }

    IEnumerator RandSpawnSelect(int _index)
    {
        randomDelay = true;
        int per = Random.Range(0, 10);
        int randB = Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.B).Count);
        int randA = Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.A).Count);
        int randS = Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.S).Count);

        Sprite randomSprite = GetRandomSprite(_index, randB, randA, randS);

        Shared.unitMng.IsCheckGround(randomUnit);

        switch (_index)
        {
            case 0:
                if (per < 6)
                {
                    randomUnit = Shared.unitMng.GetUnitByGradeList(EUnitGrade.B)[randB];
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[0], randomSprite));
                    Shared.unitMng.UnitInstantiate(randomUnit);
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[0], randomFail));
                }
                break;
            case 1:
                if (per < 2)
                {
                    randomUnit = Shared.unitMng.GetUnitByGradeList(EUnitGrade.A)[randA];
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[1], randomSprite));
                    Shared.unitMng.UnitInstantiate(randomUnit);
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[1], randomFail));
                }
                break;
            case 2:
                if (per < 1)
                {
                    randomUnit = Shared.unitMng.GetUnitByGradeList(EUnitGrade.S)[randS];
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[2], randomSprite));
                    Shared.unitMng.UnitInstantiate(randomUnit);
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[2], randomFail));
                }
                break;
        }

        yield return new WaitForSeconds(1f);

        randomDelay = false;
    }

    Sprite GetRandomSprite(int _index, int _randB, int _randA, int _randS)
    {
        switch (_index)
        {
            case 0:
                return Shared.unitMng.GetUnitByGradeList(EUnitGrade.B)[_randB].GetComponent<Unit>().UnitImg;

            case 1:
                return Shared.unitMng.GetUnitByGradeList(EUnitGrade.A)[_randA].GetComponent<Unit>().UnitImg;

            case 2:
                return Shared.unitMng.GetUnitByGradeList(EUnitGrade.S)[_randS].GetComponent<Unit>().UnitImg;
        }
        return null;
    }


    IEnumerator FadeInImage(Image _image, Sprite _sprite)
    {
        float delayTime = 0f;
        _image.sprite = _sprite;
        Color unitColor = _image.color;
        unitColor.a = 0f;
        _image.color = unitColor;

        while (delayTime < fadeTime)
        {
            delayTime += Time.deltaTime;
            unitColor.a = Mathf.Lerp(0, 1, delayTime / fadeTime);
            _image.color = unitColor;
            yield return null;
        }

        unitColor.a = 1;

        _image.sprite = null;
        Color orginColor = _image.color;
        orginColor.a = 0;
        _image.color = orginColor;
    }
}
