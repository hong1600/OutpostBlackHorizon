using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitMixer : MonoBehaviour
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;

    public List<UnitData> needUnitList = new List<UnitData>();
    public List<Unit> unitToMix = new List<Unit>();

    private void Awake()
    {
        iUnitMng = unitMng;
    }

    public bool unitCanMix()
    {
        unitToMix.Clear();

        foreach (Unit fieldUnit in iUnitMng.getCurUnitList())
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

    public void unitMixSpawn()
    {
        if (unitCanMix())
        {
            GameObject spawnUnit = Instantiate(iUnitMng.getUnitList(UnitType.SS)[GameInventoryManager.Instance.curMixUnit],
                iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform.position, Quaternion.identity,
                iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform);
            iUnitMng.getCurUnitList().Add(spawnUnit.GetComponent<Unit>());
        }
        else return;
    }
}
