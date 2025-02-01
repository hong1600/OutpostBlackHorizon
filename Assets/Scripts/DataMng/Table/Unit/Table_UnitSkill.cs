using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_UnitSkill : Table_Base
{
    [Serializable]
    public class Info
    {
        public string NameKey;
        public string Ko;
        public string En;
    }

    public Dictionary<string, Info> Dictionary = new Dictionary<string, Info>();

    public Info Get(string _id)
    {
        if (Dictionary.ContainsKey(_id))
        {
            return Dictionary[_id];
        }

        return null;
    }

    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<string, Info>>(_Name, ref Dictionary);
    }

    public void Save_Binary(string _Name)
    {
        Save_Binary(_Name, Dictionary);
    }

    public void Init_Csv(ETable _Name, int _StartRow, int _StartCol)
    {
        CSVReader reader = GetCSVReader(_Name);

        for (int row = _StartRow; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _StartCol) == false)
                break;

            Dictionary.Add(info.NameKey, info);
        }
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.getString(_Row, ref _Info.NameKey);
        _Reader.getString(_Row, ref _Info.Ko);
        _Reader.getString(_Row, ref _Info.En);

        return true;
    }
}
