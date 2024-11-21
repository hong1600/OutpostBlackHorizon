using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitSpawner 
{
    void spawnUnit();
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
        new float[] { 0.03f, 0.10f, 0.15f, 0.72f },
        new float[] { 0.05f, 0.12f, 0.18f, 0.65f },
        new float[] { 0.07f, 0.14f, 0.21f, 0.58f },
        new float[] { 0.09f, 0.16f, 0.24f, 0.51f },
        new float[] { 0.11f, 0.18f, 0.27f, 0.44f },
        new float[] { 0.13f, 0.20f, 0.30f, 0.37f }
    };
    public string[] selectOption = { "S", "A", "B", "C" };
    public int spawnGold;

    private void Awake()
    {
        iUnitMng = unitMng;
        iGoldCoin = goldCoin;
        iUnitUpgrader = unitUpgrader;

        spawnGold = 20;
    }

    public bool canSpawn()
    {
        return spawnGold <= iGoldCoin.getGold();
    }

    public void useGold()
    {
        iGoldCoin.setGold(-spawnGold);
        spawnGold += 2;
    }

    public void spawnUnit()
    {
        if (!iUnitMng.checkGround() || !canSpawn()) return;

        useGold();

        string Selection = SelectRandom(selectOption,
            selectWeight[(int)iUnitUpgrader.getUpgradeCost3() - 1]);

        int randS = Random.Range(0, iUnitMng.getUnitList(UnitType.S).Count);
        int randA = Random.Range(0, iUnitMng.getUnitList(UnitType.A).Count);
        int randB = Random.Range(0, iUnitMng.getUnitList(UnitType.B).Count);
        int randC = Random.Range(0, iUnitMng.getUnitList(UnitType.C).Count);

        switch (Selection)
        {
            case "S":
                GameObject spawnUnitS = Instantiate(iUnitMng.getUnitList(UnitType.S)[randS],
                    iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform.position,
                    Quaternion.identity, iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform);
                iUnitMng.getCurUnitList().Add(spawnUnitS.GetComponent<Unit>());
                break;
            case "A":
                GameObject spawnUnitA = Instantiate(iUnitMng.getUnitList(UnitType.A)[randA],
                    iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform.position,
                    Quaternion.identity, iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform);
                iUnitMng.getCurUnitList().Add(spawnUnitA.GetComponent<Unit>());
                break;
            case "B":
                GameObject spawnUnitB = Instantiate(iUnitMng.getUnitList(UnitType.B)[randB],
                    iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform.position,
                    Quaternion.identity, iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform);
                iUnitMng.getCurUnitList().Add(spawnUnitB.GetComponent<Unit>());
                break;
            case "C":
                GameObject spawnUnitC = Instantiate(iUnitMng.getUnitList(UnitType.C)[randC],
                    iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform.position,
                    Quaternion.identity, iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform);
                iUnitMng.getCurUnitList().Add(spawnUnitC.GetComponent<Unit>());
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
