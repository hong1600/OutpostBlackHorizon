using System;
using System.Collections.Generic;

public class Table_Character : Table_Base
{
    [Serializable]
    public class Info
    {
        public int Id;
        public byte Type;
        public int Skill;
        public int Stat;
        public string Prefabs;
        public string Img;
        public int Name;
        public int Dec;
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

    public void Init_Csv(string _Name, int _StartRow, int _StartCol)
    {
        CSVReader reader = GetCSVReader(_Name);

        for (int row = _StartRow; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _StartCol) == false)
                break ;

            Dictionary.Add(row, info);
        }
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.get(_Row, ref _Info.Id);
        _Reader.get(_Row, ref _Info.Type);
        _Reader.get(_Row, ref _Info.Skill);
        _Reader.get(_Row, ref _Info.Stat);
        _Reader.get(_Row, ref _Info.Prefabs);
        _Reader.get(_Row, ref _Info.Img);
        _Reader.get(_Row, ref _Info.Name);
        _Reader.get(_Row, ref _Info.Dec);

        return true;
    }
}
