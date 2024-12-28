using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IUnitSpawner 
{
    void spawnUnit();
    float[][] getSelectWeight();
    int getSpawnGold();
}

public class UnitSpawner : MonoBehaviour, IUnitSpawner
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;
    public UnitUpgrader unitUpgrader;
    public IUnitUpgrader iUnitUpgrader;

    public float[][] selectWeight = new float[][]
    {
    new float[] { 0.72f, 0.15f, 0.10f, 0.03f },
    new float[] { 0.65f, 0.18f, 0.12f, 0.05f },
    new float[] { 0.58f, 0.21f, 0.14f, 0.07f },
    new float[] { 0.51f, 0.24f, 0.16f, 0.09f },
    new float[] { 0.44f, 0.27f, 0.18f, 0.11f },
    new float[] { 0.37f, 0.30f, 0.20f, 0.13f }
    };

    public string[] selectOption = { "C", "B", "A", "S" };
    public int spawnGold;
    public GameObject selectSpawnUnit;

    private void Awake()
    {
        iUnitMng = unitMng;
        iGoldCoin = goldCoin;
        iUnitUpgrader = unitUpgrader;

        spawnGold = 20;
    }

    public bool canSpawn()
    {
        if (spawnGold <= iGoldCoin.getGold() && iUnitMng.getAllUnitList().Count < 20)
        {
            return true;
        }
        else return false;
    }

    public void useGold()
    {
        iGoldCoin.setGold(-spawnGold);
        spawnGold += 2;
    }

    public void spawnUnit()
    {
        string Selection = SelectRandom(selectOption, selectWeight[(int)iUnitUpgrader.getUpgradeLevel()[3] - 1]);
        selectSpawnUnit = getSelectSpawnUnit(Selection);

        if (!iUnitMng.checkGround(selectSpawnUnit) || !canSpawn()) return;

        useGold();

        iUnitMng.unitInstantiate(selectSpawnUnit);
    }

    public GameObject getSelectSpawnUnit(string grade)
    {
        switch (grade) 
        {
            case "S":
                return iUnitMng.getUnitByGradeList(EUnitGrade.S)[Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.S).Count)];
            case "A":
                return iUnitMng.getUnitByGradeList(EUnitGrade.A)[Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.A).Count)];
            case "B":
                return iUnitMng.getUnitByGradeList(EUnitGrade.B)[Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.B).Count)];
            case "C":
                return iUnitMng.getUnitByGradeList(EUnitGrade.C)[Random.Range(0, iUnitMng.getUnitByGradeList(EUnitGrade.C).Count)];
            default:
                return null;
        }
    }

    public string SelectRandom(string[] options, float[] weights)
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

    public float[][] getSelectWeight() { return selectWeight; }
    public int getSpawnGold() { return spawnGold; }
}
