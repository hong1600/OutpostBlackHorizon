using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localize : Table_Base
{
    [Serializable]
    public class Info
    {
        public string NameKey;
        public Dictionary<string, string> localDic;
    }

    public Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();

    public void Init_Csv(ETable _Name, int _StartRow, int _StartCol)
    {
        CSVReader reader = GetCSVReader(_Name);

        for (int row = _StartRow; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _StartCol) == false)
                break;

            //dictionary.Add(row, info);
        }
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        //_Reader.getString(_Row, ref _Info.NameKey);
        //_Reader.getString(_Row, ref _Info.localDic);

        return true;
    }
}
