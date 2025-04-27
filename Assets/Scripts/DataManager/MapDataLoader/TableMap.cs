using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMap : TableBase
{
    [Serializable]
    public class Info
    {
        public int ID;
        public string Name;
        public string Desc;
        public int fieldAmount;
    }

    public Dictionary<int, Info> Dictionary = new Dictionary<int, Info>();

    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<int, Info>>(_Name, ref Dictionary);
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

            Dictionary.Add(info.ID, info);
        }
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.getInt(_Row, ref _Info.ID);
        _Reader.getString(_Row, ref _Info.Name);
        _Reader.getString(_Row, ref _Info.Desc);
        _Reader.getInt(_Row, ref _Info.fieldAmount);

        return true;
    }

    public Info GetMapData(int _id)
    {
        if (Dictionary.ContainsKey(_id))
        {
            return Dictionary[_id];
        }

        return null;
    }
}
