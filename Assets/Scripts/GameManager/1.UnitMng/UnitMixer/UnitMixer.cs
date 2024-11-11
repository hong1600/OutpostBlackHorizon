using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitMixer : MonoBehaviour
{
    public UnitMng unitMng;

    public List<UnitData> needUnitList = new List<UnitData>();
    public List<Unit> unitToMix = new List<Unit>();

    private void Start()
    {
        unitMng = GetComponent<UnitMng>();
    }

    public bool unitCanMix()
    {
        unitToMix.Clear();

        foreach (Unit fieldUnit in unitMng.curUnitList)
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
            GameObject spawnUnit = Instantiate(unitMng.unitListSS[GameInventoryManager.Instance.curMixUnit],
                unitMng.unitSpawnPointList[unitMng.groundNum].transform.position, Quaternion.identity,
                unitMng.unitSpawnPointList[unitMng.groundNum].transform);
            unitMng.curUnitList.Add(spawnUnit.GetComponent<Unit>());
            GameUI.instance.mixPanel.SetActive(false);
        }
        else return;
    }
}
