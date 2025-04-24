using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class UnitRandomSpawner : MonoBehaviour
{
    UnitData unitData;
    UnitSpawner unitSpawner;
    UnitFieldData unitFieldData;
    GoldCoin goldCoin;

    [SerializeField] GameObject randomUnit;
    [SerializeField] bool randomDelay;
    [SerializeField] Sprite randomFail;
    [SerializeField] Image[] randomUnitImgs;
    [SerializeField] float fadeTime;
    [SerializeField] int spawnCoin0;
    [SerializeField] int spawnCoin1;
    [SerializeField] int spawnCoin2;

    private void Start()
    {
        unitData = UnitManager.instance.UnitData;
        unitSpawner = UnitManager.instance.UnitSpawner;
        unitFieldData = UnitManager.instance.UnitFieldData;
        goldCoin = GameManager.instance.GoldCoin;

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

    private bool CanSpawn(int _index)
    {
        List<Transform> spawnPointList = unitFieldData.GetUnitSpawnPointList();
        int spawnPointCount = spawnPointList.Count;

        for (int i = 0; i < spawnPointCount; i++)
        {
            if (spawnPointList[i].transform.childCount == 0)
            {
                switch (_index)
                {
                    case 0:
                        return spawnCoin0 <= goldCoin.GetCoin();

                    case 1:
                        return spawnCoin1 <= goldCoin.GetCoin();

                    case 2:
                        return spawnCoin2 <= goldCoin.GetCoin();

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
        int randB = Random.Range(0, unitData.GetUnitByGradeList(EUnitGrade.B).Count);
        int randA = Random.Range(0, unitData.GetUnitByGradeList(EUnitGrade.A).Count);
        int randS = Random.Range(0, unitData.GetUnitByGradeList(EUnitGrade.S).Count);

        Sprite randomSprite = GetRandomSprite(_index, randB, randA, randS);

        switch (_index)
        {
            case 0:
                if (per < 6)
                {
                    randomUnit = unitData.GetUnitByGradeList(EUnitGrade.B)[randB];
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[0], randomSprite));
                    unitSpawner.InstantiateUnit(randomUnit);
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[0], randomFail));
                }
                break;
            case 1:
                if (per < 2)
                {
                    randomUnit = unitData.GetUnitByGradeList(EUnitGrade.A)[randA];
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[1], randomSprite));
                    unitSpawner.InstantiateUnit(randomUnit);
                }
                else
                {
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[1], randomFail));
                }
                break;
            case 2:
                if (per < 1)
                {
                    randomUnit = unitData.GetUnitByGradeList(EUnitGrade.S)[randS];
                    yield return StartCoroutine(FadeInImage(randomUnitImgs[2], randomSprite));
                    unitSpawner.InstantiateUnit(randomUnit);
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
                return SpriteManager.instance.GetSprite(DataManager.instance.TableUnit.GetUnitData(102).SpriteName);

            case 1:
                return SpriteManager.instance.GetSprite(DataManager.instance.TableUnit.GetUnitData(103).SpriteName);

            case 2:
                return SpriteManager.instance.GetSprite(DataManager.instance.TableUnit.GetUnitData(104).SpriteName);
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
