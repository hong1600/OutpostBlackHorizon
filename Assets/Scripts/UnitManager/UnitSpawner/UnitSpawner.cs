using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
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

    [SerializeField] GameObject unitSkillBar;
    [SerializeField] Transform unitSkillBarParent;


    private void Awake()
    {
        spawnGold = 20;
    }

    public void InstantiateUnit(GameObject _unit)
    {
        UnitFieldData fieldData = Shared.unitManager.UnitFieldData;
        UnitData unitData = Shared.unitManager.UnitData;
        List<Transform> SpawnPointList = Shared.unitManager.UnitFieldData.GetUnitSpawnPointList();
        int fieldNum = fieldData.fieldNum;

        if (!fieldData.IsCheckGround(_unit)) return;

        if (fieldNum < 0 || fieldNum > SpawnPointList.Count || _unit == null) return;

        Vector3 twoUnit1Pos = new Vector3(-0.5f, 0, 0);
        Vector3 twoUnit2Pos = new Vector3(0.5f, 0, 0);

        Vector3 threeUnit1Pos = new Vector3(0, 0, 0.4f);
        Vector3 threeUnit2Pos = new Vector3(-0.6f, 0, -0.4f);
        Vector3 threeUnit3Pos = new Vector3(0.6f, 0, -0.4f);

        GameObject instantiateUnit;

        if (!unitData.getUnitByGroundDic.ContainsKey(fieldNum))
        {
            unitData.getUnitByGroundDic[fieldNum] = new List<GameObject>();
        }

        List<GameObject> curGroundUnit = unitData.getUnitByGroundDic[fieldNum];
        int unitCount = SpawnPointList[fieldNum].transform.childCount;

        switch (unitCount)
        {
            case 0:
                instantiateUnit = Instantiate(_unit, SpawnPointList[fieldNum].transform.position,
                    Quaternion.identity, SpawnPointList[fieldNum].transform);
                break;

            case 1:
                curGroundUnit[0].transform.position = SpawnPointList[fieldNum].transform.position + twoUnit1Pos;

                instantiateUnit = Instantiate(_unit, SpawnPointList[fieldNum].transform.position + twoUnit2Pos,
                    Quaternion.identity, SpawnPointList[fieldNum].transform);
                break;

            case 2:
                curGroundUnit[0].transform.position = SpawnPointList[fieldNum].transform.position + threeUnit1Pos;
                curGroundUnit[1].transform.position = SpawnPointList[fieldNum].transform.position + threeUnit2Pos;

                instantiateUnit = Instantiate(_unit, SpawnPointList[fieldNum].transform.position + threeUnit3Pos,
                    Quaternion.identity, SpawnPointList[fieldNum].transform);
                break;

            default:
                return;
        }

        if (instantiateUnit.GetComponent<Unit>().eUnitGrade == EUnitGrade.SS ||
            instantiateUnit.GetComponent<Unit>().eUnitGrade == EUnitGrade.S)
        {
            GameObject skillBar = Instantiate(unitSkillBar, instantiateUnit.transform.position,
                Quaternion.identity, unitSkillBarParent);
            skillBar.GetComponent<UnitSkillBar>().Init(instantiateUnit.GetComponent<Unit>());
            instantiateUnit.GetComponent<Unit>().skillBar = skillBar;
        }

        unitData.AddUnitData(instantiateUnit);
    }

    public bool CanSpawn()
    {
        if (spawnGold <= Shared.gameManager.GoldCoin.GetGold() &&
            Shared.unitManager.UnitData.GetAllUnitList().Count < 20)
        {
            return true;
        }
        else return false;
    }

    public void UseGold()
    {
        Shared.gameManager.GoldCoin.SetGold(-spawnGold);
        spawnGold += 2;
    }

    public void SpawnUnit()
    {
        string Selection = 
            SelectRandom(selectOptions, selectWeights[(int)Shared.unitManager.UnitUpgrader.GetUpgradeLevel()[3] - 1]);
        selectSpawnUnit = GetSelectSpawnUnit(Selection);

        if (!CanSpawn()) return;

        InstantiateUnit(selectSpawnUnit);

        UseGold();

    }

    public GameObject GetSelectSpawnUnit(string grade)
    {
        switch (grade) 
        {
            case "S":
                return Shared.unitManager.UnitData.GetUnitByGradeList(EUnitGrade.S)
                    [Random.Range(0, Shared.unitManager.UnitData.GetUnitByGradeList(EUnitGrade.S).Count)];
            case "A":
                return Shared.unitManager.UnitData.GetUnitByGradeList(EUnitGrade.A)
                    [Random.Range(0, Shared.unitManager.UnitData.GetUnitByGradeList(EUnitGrade.A).Count)];
            case "B":
                return Shared.unitManager.UnitData.GetUnitByGradeList(EUnitGrade.B)
                    [Random.Range(0, Shared.unitManager.UnitData.GetUnitByGradeList(EUnitGrade.B).Count)];
            case "C":
                return Shared.unitManager.UnitData.GetUnitByGradeList(EUnitGrade.C)
                    [Random.Range(0, Shared.unitManager.UnitData.GetUnitByGradeList(EUnitGrade.C).Count)];
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
