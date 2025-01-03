using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IUnitMixer
{
    IEnumerator StartUnitMixSpawn();
    bool IsUnitCanMix();
}

public class UnitMixer : MonoBehaviour, IUnitMixer
{
    public List<Unit> unitToMixList = new List<Unit>();

    public GameObject mixPanel;

    public bool IsUnitCanMix()
    {
        unitToMixList.Clear();

        foreach (var fieldUnit in Shared.unitMng.GetAllUnitList())
        {
            Unit field = fieldUnit.GetComponent<Unit>();

            foreach (UnitData needUnit in Shared.gameUI.iUIMixRightSlot.GetSacUnitList())
            {
                if (field.unitName == needUnit.unitName &&
                    !unitToMixList.Any(unit => unit.unitName == field.unitName))
                {
                    unitToMixList.Add(field);
                }
            }
        }

        if(unitToMixList.Count > 0 && unitToMixList.Count == Shared.gameUI.iUIMixRightSlot.GetSacUnitList().Count) 
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
                Shared.unitMng.GetUnitByGradeList(EUnitGrade.SS)[Shared.gameUI.iUIMixRightSlot.GetCurMixUnit()];

            Shared.unitMng.IsCheckGround(spawnUnit);

            Shared.unitMng.UnitInstantiate(spawnUnit);

            mixPanel.SetActive(false);
        }
        else yield break;
    }
}
