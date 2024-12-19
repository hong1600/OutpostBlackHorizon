using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IUnitMixer
{
    IEnumerator unitMixSpawn();
    bool unitCanMix();
}

public class UnitMixer : MonoBehaviour, IUnitMixer
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;
    public UIMixRightSlot rightSlot;
    public iUIMixRightSlot iRightSlot;

    public List<Unit> unitToMix = new List<Unit>();

    public GameObject mixPanel;

    private void Awake()
    {
        iUnitMng = unitMng;
        iRightSlot = rightSlot;
    }

    public bool unitCanMix()
    {
        unitToMix.Clear();


        foreach (Unit fieldUnit in iUnitMng.getCurUnitList())
        {
            foreach (UnitData needUnit in iRightSlot.getNeedUnitList())
            {
                if (fieldUnit.unitName == needUnit.unitName &&
                    !unitToMix.Any(unit => unit.unitName == fieldUnit.unitName))
                {
                    unitToMix.Add(fieldUnit);
                }
            }
        }

        if( unitToMix.Count > 0 && unitToMix.Count == iRightSlot.getNeedUnitList().Count) 
        {
            return true;
        }

        return false;
    }

    public IEnumerator unitMixSpawn()
    {
        if (unitCanMix())
        {
            for (int i = 0; i < unitToMix.Count; i++)
            {
                Destroy(unitToMix[i].gameObject);
            }

            yield return new WaitForEndOfFrame();

            GameObject spawnUnit = iUnitMng.getUnitList(EUnitGrade.SS)[iRightSlot.getCurMixUnit()];

            iUnitMng.checkGround(spawnUnit);

            iUnitMng.unitInstantiate(spawnUnit);

            mixPanel.SetActive(false);
        }
        else yield break;
    }
}
