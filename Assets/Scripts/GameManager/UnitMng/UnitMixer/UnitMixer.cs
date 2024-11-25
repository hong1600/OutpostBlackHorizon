using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IUnitMixer
{
    void unitMixSpawn();
}

public class UnitMixer : MonoBehaviour, IUnitMixer
{
    public UnitMng unitMng;
    public IUnitMng iUnitMng;
    public GameUIMixRightSlot rightSlot;
    public iGameUIMixRightSlot iRightSlot;

    public List<UnitData> needUnitList = new List<UnitData>();
    public List<Unit> unitToMix = new List<Unit>();

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

        return unitToMix.Count == needUnitList.Count;
    }

    public void unitMixSpawn()
    {
        unitCanMix();

        if (unitCanMix())
        {
            GameObject spawnUnit = Instantiate(iUnitMng.getUnitList(UnitType.SS)[iRightSlot.getCurMixUnit()],
                iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform.position, Quaternion.identity,
                iUnitMng.getUnitSpawnPointList()[iUnitMng.getGroundNum()].transform);
            iUnitMng.getCurUnitList().Add(spawnUnit.GetComponent<Unit>());
        }
        else return;
    }
}
