using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

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

        List<GameObject> allUnitList = Shared.unitMng.iUnitMng.GetAllUnitList();
        List<UnitData> sacUnitList = Shared.gameUI.iUIMixRightSlot.GetSacUnitList();

        foreach (var fieldUnit in allUnitList)
        {
            Unit field = fieldUnit.GetComponent<Unit>();

            foreach (UnitData needUnit in sacUnitList)
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

            Shared.unitMng.iUnitMng.UnitInstantiate(spawnUnit);

            mixPanel.SetActive(false);
        }
        else yield break;
    }
}
