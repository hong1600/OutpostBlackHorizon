using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitMixer : MonoBehaviour
{
    public List<Unit> unitToMixList = new List<Unit>();

    public GameObject mixPanel;

    public bool IsUnitCanMix()
    {
        unitToMixList.Clear();

        List<GameObject> allUnitList = Shared.unitMng.GetAllUnitList();
        List<TableUnit.Info> sacUnitList = Shared.gameUI.UIMixRightSlot.GetUnitList();

        foreach (GameObject fieldUnit in allUnitList)
        {
            Unit field = fieldUnit.GetComponent<Unit>();

            foreach (TableUnit.Info needUnit in sacUnitList)
            {
                if (field.unitName == needUnit.Name &&
                    !unitToMixList.Any(unit => unit.unitName == field.unitName))
                {
                    unitToMixList.Add(field);
                }
            }
        }

        if(unitToMixList.Count > 0 && unitToMixList.Count == Shared.gameUI.UIMixRightSlot.GetUnitList().Count) 
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
                Shared.unitMng.GetUnitByGradeList(EUnitGrade.SS)[Shared.gameUI.UIMixRightSlot.GetCurMixUnit()];

            Shared.unitMng.UnitInstantiate(spawnUnit);

            mixPanel.SetActive(false);
        }
        else yield break;
    }
}
