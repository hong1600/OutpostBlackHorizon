using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitMixer : MonoBehaviour
{
    UnitData unitData;
    UnitSpawner unitSpawner;

    GameUI gameUI;

    public List<Unit> unitToMixList = new List<Unit>();

    public GameObject mixPanel;

    private void Start()
    {
        gameUI = GameUI.instance;
        unitData = UnitManager.instance.UnitData;
        unitSpawner = UnitManager.instance.UnitSpawner;
    }

    public bool IsUnitCanMix()
    {
        unitToMixList.Clear();

        List<GameObject> allUnitList = unitData.GetAllUnitList();
        List<TableUnit.Info> sacUnitList = gameUI.UIMixRightSlot.GetUnitList();

        foreach (GameObject fieldUnit in allUnitList)
        {
            Unit unit = fieldUnit.GetComponent<Unit>();

            foreach (TableUnit.Info needUnit in sacUnitList)
            {
                if (unit.defenderName == needUnit.Name &&
                    !unitToMixList.Any(unit => unit.defenderName == needUnit.Name))
                {
                    unitToMixList.Add(unit);
                }
            }
        }

        if(unitToMixList.Count > 0 && unitToMixList.Count == gameUI.UIMixRightSlot.GetUnitList().Count) 
        {
            return true;
        }

        return false;
    }

    public IEnumerator StartUnitMixSpawn()
    {
        if (IsUnitCanMix())
        {
            for (int i = 0; i < unitToMixList.Count; i++)
            {
                Destroy(unitToMixList[i].gameObject);
            }

            yield return new WaitForEndOfFrame();

            GameObject spawnUnit =
                unitData.GetUnitByGradeList(EUnitGrade.SS)[gameUI.UIMixRightSlot.GetCurMixUnit()];

            unitSpawner.InstantiateUnit(spawnUnit);

            mixPanel.SetActive(false);
        }
        else yield break;
    }
}
