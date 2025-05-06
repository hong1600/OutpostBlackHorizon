using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableEnemy : TableBase
{
    [Serializable]
    public class Info
    {
        public int ID;
        public string Name;
        public string Enum;
        public int Speed;
        public int MaxHp;
        public string HpBarPath;
        public int AttackRange;
        public int AttackDmg;
        public string Desc;
    }

    public Dictionary<EEnemy, Info> Dictionary = new Dictionary<EEnemy, Info>();

    public Info Get(EEnemy _eEnemy)
    {
        if (Dictionary.ContainsKey(_eEnemy))
        {
            return Dictionary[_eEnemy];
        }

        return null;
    }

    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<EEnemy, Info>>(_Name, ref Dictionary);
    }

    public void Save_Binary(string _Name)
    {
        Save_Binary(_Name, Dictionary);
    }

    public void Init_Csv(ETable _Name, int _StartRow, int _StartCol)
    {
        Dictionary.Clear();

        CSVReader reader = GetCSVReader(_Name);

        for (int row = _StartRow; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _StartCol) == false)
                break;

            if (Enum.TryParse(info.Enum, out EEnemy eEnemy))
            {
                Dictionary.Add(eEnemy, info);
            }
        }
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.getInt(_Row, ref _Info.ID);
        _Reader.getString(_Row, ref _Info.Name);
        _Reader.getString(_Row, ref _Info.Enum);
        _Reader.getInt(_Row, ref _Info.Speed);
        _Reader.getInt(_Row, ref _Info.MaxHp);
        _Reader.getString(_Row, ref _Info.HpBarPath);
        _Reader.getInt(_Row, ref _Info.AttackRange);
        _Reader.getInt(_Row, ref _Info.AttackDmg);
        _Reader.getString(_Row, ref _Info.Desc);

        return true;
    }
}
