using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitMngData", menuName = "UnitManager/UnitMngData", order = 0)]
public class UnitMngData : ScriptableObject
{
    public List<Unit> curUnitList = new List<Unit>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
    public List<GameObject> unitListSS, unitListS, unitListA, unitListB, unitListC;
    public int groundNum;
    public int spawnGold;
    public int coin;
}
