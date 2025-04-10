using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Singleton<FieldManager>
{
    [SerializeField] FieldBuild fieldBuild;
    [SerializeField] TurretBuild turretBuild;
    [SerializeField] FieldSelector fieldSelector;

    [SerializeField] List<FieldData> fieldDataList = new List<FieldData>(); 
    Dictionary<FieldData, int> fieldAmountDic = new Dictionary<FieldData, int>();

    protected override void Awake()
    {
        base.Awake();

        FieldBuild = fieldBuild;
        TurretBuild = turretBuild;
        FieldSelector = fieldSelector;

        InitField(fieldDataList);
    }

    public void InitField(List<FieldData> _fieldDataList)
    {
        for(int i = 0; i < _fieldDataList.Count; i++) 
        {
            FieldData fieldData = _fieldDataList[i];
            fieldAmountDic[fieldData] = fieldData.fieldAmount;
        }
    }

    public int GetFieldAmount(FieldData _fieldData)
    {
        return fieldAmountDic.ContainsKey(_fieldData) ? fieldAmountDic[_fieldData] : 0;
    }

    public void DecreaseFieldAmount(int _fieldAmount)
    {
        if (_fieldAmount > 0)
        {
            _fieldAmount--;
        }
    }

    public FieldBuild FieldBuild { get; private set; }
    public TurretBuild TurretBuild { get; private set; }
    public FieldSelector FieldSelector { get; private set; }
}
