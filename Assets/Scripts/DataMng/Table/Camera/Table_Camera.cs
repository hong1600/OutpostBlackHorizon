using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Camera : Table_Base
{
    [Serializable]
    public class Info
    {
        public int ID;
        public float delay;
        public float shakeTime;
        public float shakeX;
        public float shakeY;
        public float shakeSpd;
        public float damping;
        public float maxShakeCount;
        public float local;
    }

    public Dictionary<int, Info> Dictionary = new Dictionary<int, Info>();

    public Info Get(int _id)
    {
        if (Dictionary.ContainsKey(_id))
        {
            return Dictionary[_id];
        }

        return null;
    }

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
        CSVReader reader = GetCSVReader(_Name);

        for (int row = _StartRow; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _StartCol) == false)
                break;

            Dictionary.Add(row, info);
        }
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.getInt(_Row, ref _Info.ID);
        _Reader.getFloat(_Row, ref _Info.delay);
        _Reader.getFloat(_Row, ref _Info.shakeTime);
        _Reader.getFloat(_Row, ref _Info.shakeX);
        _Reader.getFloat(_Row, ref _Info.shakeY);
        _Reader.getFloat(_Row, ref _Info.shakeSpd);
        _Reader.getFloat(_Row, ref _Info.damping);
        _Reader.getFloat(_Row, ref _Info.maxShakeCount);
        _Reader.getFloat(_Row, ref _Info.local);

        return true;
    }
}
