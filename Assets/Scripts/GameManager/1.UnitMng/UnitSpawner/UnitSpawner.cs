using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public UnitMng unitMng;

    public float[][] selectWeight = new float[][]
{
        new float[] { 0.03f, 0.10f, 0.15f, 0.72f },
        new float[] { 0.05f, 0.12f, 0.18f, 0.65f },
        new float[] { 0.07f, 0.14f, 0.21f, 0.58f },
        new float[] { 0.09f, 0.16f, 0.24f, 0.51f },
        new float[] { 0.11f, 0.18f, 0.27f, 0.44f },
        new float[] { 0.13f, 0.20f, 0.30f, 0.37f }
};
    public string[] selectOption = { "S", "A", "B", "C" };
    public int spawnGold;

    private void Start()
    {
        spawnGold = 20;

        unitMng = GetComponent<UnitMng>();
    }

    public bool canSpawn()
    {
        return spawnGold <= GameManager.Instance.myGold;
    }

    public void useGold()
    {
        GameManager.Instance.myGold -= spawnGold;
        spawnGold += 2;
    }

    public void spawnUnit()
    {
        if (!unitMng.checkGround() || !canSpawn()) return;

        useGold();

        string Selection = SelectRandom(selectOption,
            selectWeight[(int)GameManager.Instance.unitMng.unitUpgrader.upgradeLevel3 - 1]);

        int randS = Random.Range(0, unitMng.unitListS.Count);
        int randA = Random.Range(0, unitMng.unitListA.Count);
        int randB = Random.Range(0, unitMng.unitListB.Count);
        int randC = Random.Range(0, unitMng.unitListC.Count);

        switch (Selection)
        {
            case "S":
                GameObject spawnUnitS = Instantiate(unitMng.unitListS[randS],
                    unitMng.unitSpawnPointList[unitMng.groundNum].transform.position,
                    Quaternion.identity, unitMng.unitSpawnPointList[unitMng.groundNum].transform);
                unitMng.curUnitList.Add(spawnUnitS.GetComponent<Unit>());
                break;
            case "A":
                GameObject spawnUnitA = Instantiate(unitMng.unitListA[randA],
                    unitMng.unitSpawnPointList[unitMng.groundNum].transform.position,
                    Quaternion.identity, unitMng.unitSpawnPointList[unitMng.groundNum].transform);
                unitMng.curUnitList.Add(spawnUnitA.GetComponent<Unit>());
                break;
            case "B":
                GameObject spawnUnitB = Instantiate(unitMng.unitListB[randB],
                    unitMng.unitSpawnPointList[unitMng.groundNum].transform.position,
                    Quaternion.identity, unitMng.unitSpawnPointList[unitMng.groundNum].transform);
                unitMng.curUnitList.Add(spawnUnitB.GetComponent<Unit>());
                break;
            case "C":
                GameObject spawnUnitC = Instantiate(unitMng.unitListC[randC],
                    unitMng.unitSpawnPointList[unitMng.groundNum].transform.position,
                    Quaternion.identity, unitMng.unitSpawnPointList[unitMng.groundNum].transform);
                unitMng.curUnitList.Add(spawnUnitC.GetComponent<Unit>());
                break;
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
}
