using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFieldData : MonoBehaviour
{
    [SerializeField] List<Transform> unitSpawnPointList = new List<Transform>();
    public int fieldNum;

    public bool IsCheckGround(GameObject _spawnUnit)
    {
        fieldNum = 0;

        for (fieldNum = 0; fieldNum < unitSpawnPointList.Count; fieldNum++)
        {
            var ground = unitSpawnPointList[fieldNum].transform;

            if (ground.childCount < 3 && ground.childCount > 0)
            {
                if (ground.GetChild(0).name == $"{_spawnUnit.name}(Clone)")
                {
                    return true;
                }
            }
        }

        for (fieldNum = 0; fieldNum < unitSpawnPointList.Count; fieldNum++)
        {
            var ground = unitSpawnPointList[fieldNum].transform;

            if (ground.transform.childCount == 0)
            {
                return true;
            }
        }

        return false;
    }

    public int GetSelectGroundNum(Transform _groundTrs)
    {
        if (_groundTrs != null)
        {
            Ground ground = _groundTrs.GetComponent<Ground>();

            if (ground != null)
            {
                return (int)ground.eGround;
            }
        }

        return -1;
    }

    public List<Transform> GetUnitSpawnPointList() { return unitSpawnPointList; }
}
