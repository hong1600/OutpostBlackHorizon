using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSpawnerData", menuName = "UnitManager/UnitSpawnerData", order = 3)]
public class UnitSpawnerData : ScriptableObject
{
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
    public int spawnGold;
}
