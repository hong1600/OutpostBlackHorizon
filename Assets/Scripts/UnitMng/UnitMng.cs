using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUnitMng 
{
    event Action onUnitCountChange;
    int GetGroundNum();
    bool IsCheckGround(GameObject _spawnUnit);
    void UnitInstantiate(GameObject _unit);
    List<GameObject> GetUnitByGradeList(EUnitGrade _unitType);
    List<GameObject> GetUnitByGroundList(int _groundNum);
    List<GameObject> GetUnitSpawnPointList();
    List<GameObject> GetAllUnitList();
    void RemoveUnitData(GameObject _unit);
}

public class UnitMng : MonoBehaviour, IUnitMng
{
    public event Action onUnitCountChange;

    public UnitFusion unitFusion;
    public IUnitFusion iUnitFusion;
    public UnitMixer unitMixer;
    public IUnitMixer iUnitMixer;
    public UnitRandomSpawner unitRandomSpawner;
    public IUnitRandomSpawner iUnitRandomSpawner;
    public UnitSpawner unitSpawner;
    public IUnitSpawner iUnitSpawner;
    public UnitUpgrader unitUpgrader;
    public IUnitUpgrader iUnitUpgrader;
    public UnitFieldMove unitFieldMove;
    public IUnitFieldMove iUnitFieldMove;

    public List<GameObject> allUnitList = new List<GameObject>();
    public List<GameObject> unitCList, unitBList, unitAList, unitSList, unitSSList = new List<GameObject>();
    public Dictionary<int, List<GameObject>> unitByGroundDic = new Dictionary<int, List<GameObject>>();
    public List<GameObject> unitSpawnPointList = new List<GameObject>();
    public int groundNum;
    [SerializeField] GameObject unitSkillBar;
    [SerializeField] Transform unitSkillBarParent;

    private void Awake()
    {
        if (Shared.unitMng == null)
        {
            Shared.unitMng = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        iUnitFusion = unitFusion;
        iUnitMixer = unitMixer;
        iUnitRandomSpawner = unitRandomSpawner;
        iUnitSpawner = unitSpawner;
        iUnitUpgrader = unitUpgrader;
        iUnitFieldMove = unitFieldMove;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnitInstantiate(unitCList[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnitInstantiate(unitBList[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnitInstantiate(unitAList[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UnitInstantiate(unitSList[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UnitInstantiate(unitSSList[0]);
        }
    }

    public bool IsCheckGround(GameObject _spawnUnit)
    {
        groundNum = 0;

        for (groundNum = 0; groundNum < unitSpawnPointList.Count; groundNum++)
        {
            var ground = unitSpawnPointList[groundNum].transform;

            if (ground.childCount < 3 && ground.childCount > 0)
            {
                if (ground.GetChild(0).name == $"{_spawnUnit.name}(Clone)")
                {
                    return true;
                }
            }
        }

        for(groundNum = 0 ;groundNum < unitSpawnPointList.Count; groundNum++) 
        {
            var ground = unitSpawnPointList[groundNum].transform;

            if (ground.transform.childCount == 0)
            {
                return true;
            }
        }

        return false;
    }

    public void UnitInstantiate(GameObject _unit)
    {
        if (groundNum < 0 || groundNum > unitSpawnPointList.Count || _unit == null) return;

        Vector3 twoUnit1Pos = new Vector3(-0.5f, 0, 0);
        Vector3 twoUnit2Pos = new Vector3(0.5f, 0, 0);

        Vector3 threeUnit1Pos = new Vector3 (0, 0, 0.4f);
        Vector3 threeUnit2Pos = new Vector3 (-0.6f, 0, -0.4f);
        Vector3 threeUnit3Pos = new Vector3 (0.6f, 0, -0.4f);

        GameObject instantiateUnit;

        if (!unitByGroundDic.ContainsKey(groundNum))
        {
            unitByGroundDic[groundNum] = new List<GameObject>();
        }

        List<GameObject> curGroundUnit = unitByGroundDic[groundNum];
        int unitCount = unitSpawnPointList[groundNum].transform.childCount;

        switch (unitCount) 
        {
            case 0:
                instantiateUnit = Instantiate(_unit, unitSpawnPointList[groundNum].transform.position,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;

            case 1:
                curGroundUnit[0].transform.position = unitSpawnPointList[groundNum].transform.position + twoUnit1Pos;

                instantiateUnit = Instantiate(_unit, unitSpawnPointList[groundNum].transform.position + twoUnit2Pos,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;

            case 2:
                curGroundUnit[0].transform.position = unitSpawnPointList[groundNum].transform.position + threeUnit1Pos;
                curGroundUnit[1].transform.position = unitSpawnPointList[groundNum].transform.position + threeUnit2Pos;

                instantiateUnit = Instantiate(_unit, unitSpawnPointList[groundNum].transform.position + threeUnit3Pos,
                    Quaternion.identity, unitSpawnPointList[groundNum].transform);
                break;

            default:
                return;
        }

        if (instantiateUnit.GetComponent<Unit>().eUnitGrade == EUnitGrade.SS ||
            instantiateUnit.GetComponent<Unit>().eUnitGrade == EUnitGrade.S)
        {
            GameObject skillBar = Instantiate(unitSkillBar, instantiateUnit.transform.position,
                Quaternion.identity, unitSkillBarParent);
            skillBar.GetComponent<UnitSkillBar>().Init(instantiateUnit.GetComponent<Unit>());
            instantiateUnit.GetComponent<Unit>().skillBar = skillBar;
        }

        AddUnitData(instantiateUnit);
    }

    public void AddUnitData(GameObject _unit)
    {
        allUnitList.Add(_unit);

        unitByGroundDic[groundNum].Add(_unit);

        onUnitCountChange?.Invoke();
    }

    public void RemoveUnitData(GameObject _unit)
    {
        Transform groundTrs = _unit.transform.parent.parent;
        groundNum = GetSelectGroundNum(groundTrs);

        allUnitList.Remove(_unit);
        Destroy(_unit);

        unitByGroundDic[groundNum].Clear();

        onUnitCountChange?.Invoke();
    }

    public List<GameObject> GetUnitByGroundList(int _groundNum)
    {
        if (!unitByGroundDic.ContainsKey(_groundNum)) return null;

        return unitByGroundDic[_groundNum];
    }

    public List<GameObject> GetUnitByGradeList(EUnitGrade _unitType)
    {
        switch (_unitType)
        {
            case EUnitGrade.SS:
                return unitSSList;
            case EUnitGrade.S:
                return unitSList;
            case EUnitGrade.A:
                return unitAList;
            case EUnitGrade.B:
                return unitBList;
            case EUnitGrade.C:
                return unitCList;
                default: 
                return null;
        }
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

    public List<GameObject> GetUnitSpawnPointList() { return unitSpawnPointList; }
    public int GetGroundNum() { return groundNum; }
    public List<GameObject> GetAllUnitList() { return allUnitList; }
}
