using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IUnitMixer
{
    void unitMixSpawn();
    bool unitCanMix();
}

public class UnitMixer : MonoBehaviour, IUnitMixer
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;
    public UIMixRightSlot rightSlot;
    public iUIMixRightSlot iRightSlot;

    public List<UnitData> needUnitList = new List<UnitData>();
    public List<Unit> unitToMix = new List<Unit>();

    public GameObject mixPanel;

    private void Awake()
    {
        iUnitMng = unitMng;
        iRightSlot = rightSlot;
    }

    public bool unitCanMix()
    {
        //List<UnitData> needUnitList = iRightSlot.getNeedUnitList();

        //for (int i = 0; i < iUnitMng.getCurUnitList().Count; i++)
        //{
        //    Unit fieldUnit = iUnitMng.getCurUnitList()[i];

        //    for (int j = 0; j < needUnitList.Count; j++)
        //    {
        //        UnitData needUnit = needUnitList[j];

        //        if(fieldUnit.unitName == needUnit.unitName && )
        //    }
        //}

        unitToMix.Clear();

        needUnitList = iRightSlot.getNeedUnitList();

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

        if( unitToMix.Count > 0 && unitToMix.Count == needUnitList.Count) 
        {
            return true;
        }

        return false;
    }

    public void unitMixSpawn()
    {
        unitCanMix();

        if (unitCanMix())
        {
            iUnitMng.checkGround();
            GameObject spawnUnit = Instantiate(iUnitMng.getUnitList(EUnitGrade.SS)[iRightSlot.getCurMixUnit()],
                iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform.position, Quaternion.identity,
                iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform);
                iUnitMng.getCurUnitList().Add(spawnUnit.GetComponent<Unit>());

            mixPanel.SetActive(false);
        }
        else return;
    }
}
