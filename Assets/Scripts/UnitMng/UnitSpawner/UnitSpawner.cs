using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IUnitSpawner 
{
    void SpawnUnit();
    float[][] GetSelectWeight();
    int GetSpawnGold();
}

public class UnitSpawner : MonoBehaviour, IUnitSpawner
{
    [SerializeField] float[][] selectWeights = new float[][]
{
    new float[] { 0.72f, 0.15f, 0.10f, 0.03f },
    new float[] { 0.65f, 0.18f, 0.12f, 0.05f },
    new float[] { 0.58f, 0.21f, 0.14f, 0.07f },
    new float[] { 0.51f, 0.24f, 0.16f, 0.09f },
    new float[] { 0.44f, 0.27f, 0.18f, 0.11f },
    new float[] { 0.37f, 0.30f, 0.20f, 0.13f }
};
    [SerializeField] string[] selectOptions = { "C", "B", "A", "S" };
    [SerializeField] int spawnGold;
    [SerializeField] GameObject selectSpawnUnit;

    private void Awake()
    {
        spawnGold = 20;
    }

    public bool CanSpawn()
    {
        if (spawnGold <= Shared.gameMng.GoldCoin.GetGold() && Shared.unitMng.GetAllUnitList().Count < 20)
        {
            return true;
        }
        else return false;
    }

    public void UseGold()
    {
        Shared.gameMng.GoldCoin.SetGold(-spawnGold);
        spawnGold += 2;
    }

    public void SpawnUnit()
    {
        string Selection = 
            SelectRandom(selectOptions, selectWeights[(int)Shared.unitMng.iUnitUpgrader.GetUpgradeLevel()[3] - 1]);
        selectSpawnUnit = GetSelectSpawnUnit(Selection);

        if (!CanSpawn()) return;

        Shared.unitMng.UnitInstantiate(selectSpawnUnit);

        UseGold();

    }

    public GameObject GetSelectSpawnUnit(string grade)
    {
        switch (grade) 
        {
            case "S":
                return Shared.unitMng.GetUnitByGradeList(EUnitGrade.S)
                    [Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.S).Count)];
            case "A":
                return Shared.unitMng.GetUnitByGradeList(EUnitGrade.A)
                    [Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.A).Count)];
            case "B":
                return Shared.unitMng.GetUnitByGradeList(EUnitGrade.B)
                    [Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.B).Count)];
            case "C":
                return Shared.unitMng.GetUnitByGradeList(EUnitGrade.C)
                    [Random.Range(0, Shared.unitMng.GetUnitByGradeList(EUnitGrade.C).Count)];
            default:
                return null;
        }
    }

    public string SelectRandom(string[] _options, float[] _weights)
    {
        float totalWeight = 0f;

        foreach (float weight in _weights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < _options.Length; i++)
        {
            cumulativeWeight += _weights[i];

            if (randomValue < cumulativeWeight)
            {
                return _options[i];
            }
        }

        return _options[_options.Length - 1];
    }

    public float[][] GetSelectWeight() { return selectWeights; }
    public int GetSpawnGold() { return spawnGold; }
}
