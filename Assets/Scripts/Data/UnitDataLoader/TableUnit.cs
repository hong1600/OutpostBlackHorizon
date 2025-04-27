using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class TableUnit : TableBase
{
    [Serializable]
    public class Info
    {
        public int ID;
        public string Name;
        public EUnitGrade Grade;
        public int Damage;
        public int AttackSpeed;
        public int AttackRange;
        public string SpriteName;
        public string Skill1Name;
        public string Skill2Name;
        public int Level;
        public int CurExp;
        public int MaxExp;
        public int UpgrdaeCost;
        public int StoreCost;
        public string MixUnitID;
        public string Desc;

        [System.NonSerialized]
        public TableUnit.Info[] mixNeedUnits;
        public Table_UnitName.Info UnitLocalName;
    }

    public Dictionary<int, Info> Dictionary = new Dictionary<int, Info>();
    public Dictionary<EUnitGrade, List<Info>> unitGradeDataDic = new Dictionary<EUnitGrade, List<Info>>();

    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<int, Info>>(_Name, ref Dictionary);
        UnitGradeData();
    }

    public void Save_Binary(string _Name)
    {
        Save_Binary(_Name, Dictionary);
    }

    public void Init_Csv(ETable _Name, int _StartRow, int _StartCol)
    {
        Dictionary.Clear();
        unitGradeDataDic.Clear();

        CSVReader reader = GetCSVReader(_Name);

        for (int row = _StartRow; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _StartCol) == false)
                break ;
             
            Dictionary.Add(info.ID, info);
        }

        int i = 0;
        List<int> keys = new List<int>(Dictionary.Keys);

        while (i < Dictionary.Count)
        {
            int unitID = keys[i];
            Info unit = Dictionary[unitID];

            if (!string.IsNullOrEmpty(unit.MixUnitID))
            {
                string[] mixUnitIDs = unit.MixUnitID.Split('/');
                List<TableUnit.Info> mixUnitList = new List<TableUnit.Info>();

                foreach (var mixUnitName in mixUnitIDs)
                {
                    if (int.TryParse(mixUnitName.Trim(), out int mixId) && Dictionary.ContainsKey(mixId))
                    {
                        mixUnitList.Add(Dictionary[mixId]);
                    }
                }

                unit.mixNeedUnits = mixUnitList.ToArray();
            }
            i++;
        }
        UnitGradeData();
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.getInt(_Row, ref _Info.ID);
        _Reader.getString(_Row, ref _Info.Name);
        _Reader.getEnum<EUnitGrade>(_Row, ref _Info.Grade);
        _Reader.getInt(_Row, ref _Info.Damage);
        _Reader.getInt(_Row, ref _Info.AttackSpeed);
        _Reader.getInt(_Row, ref _Info.AttackRange);
        _Reader.getString(_Row, ref _Info.SpriteName);
        _Reader.getString(_Row, ref _Info.Skill1Name);
        _Reader.getString(_Row, ref _Info.Skill2Name);
        _Reader.getInt(_Row, ref _Info.Level);
        _Reader.getInt(_Row, ref _Info.CurExp);
        _Reader.getInt(_Row, ref _Info.MaxExp);
        _Reader.getInt(_Row, ref _Info.UpgrdaeCost);
        _Reader.getInt(_Row, ref _Info.StoreCost);
        _Reader.getString(_Row, ref _Info.MixUnitID);
        _Reader.getString(_Row, ref _Info.Desc);

        return true;
    }

    public void UnitGradeData()
    {
        unitGradeDataDic.Clear();

        foreach (var unitInfo in Dictionary.Values)
        {
            if (!unitGradeDataDic.ContainsKey(unitInfo.Grade))
            {
                unitGradeDataDic[unitInfo.Grade] = new List<Info>();
            }
            unitGradeDataDic[unitInfo.Grade].Add(unitInfo);
        }
    }

    public Info GetUnitData(int _id)
    {
        if (Dictionary.ContainsKey(_id))
        {
            return Dictionary[_id];
        }

        return null;
    }

    public List<Info> GetUnitByGradeData(EUnitGrade _grade)
    {
        if (unitGradeDataDic.ContainsKey(_grade))
        {
            return unitGradeDataDic[_grade];
        }
        return null;
    }

    public void LinkUnitName(Table_UnitName _unitName)
    {
        foreach (TableUnit.Info unit in Dictionary.Values)
        {
            if (_unitName.Dictionary.TryGetValue(unit.Name, out Table_UnitName.Info unitName))
            {
                unit.UnitLocalName = unitName;
            }
        }
    }

    //System.Func<object, bool> UpdateAction = null;
    //UpdateAction = 함수연결
    //System.Action<bool> FinishAction = null;
    //FinishAction = 함수연결
    //delegate void CallBack();
    //CallBack callback = null;
    //callback = 함수연결
    //async => await 비동기
    //변수 호출시 연결된 함수 실행
}
