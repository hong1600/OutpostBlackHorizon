using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static UnityEngine.Rendering.VolumeComponent;

public class Table_Unit : Table_Base
{
    [Serializable]
    public class Info
    {
        public int ID;
        public string Name;
        public string Grade;
        public int Damage;
        public int AttackSpeed;
        public int AttackRange;
        public string Img;
        public string Skill1Name;
        public string Skill2Name;
        public int Level;
        public int CurExp;
        public int MaxExp;
        public int UpgrdaeCost;
        public int StoreCost;
        public string MixNeedUnit1;
        public string MixNeedUnit2;
        public string MixNeedUnit3;
        public string Desc;
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
                break ;
             
            Dictionary.Add(row, info);
        }
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.getInt(_Row, ref _Info.ID);
        _Reader.getString(_Row, ref _Info.Name);
        _Reader.getString(_Row, ref _Info.Grade);
        _Reader.getInt(_Row, ref _Info.Damage);
        _Reader.getInt(_Row, ref _Info.AttackSpeed);
        _Reader.getInt(_Row, ref _Info.AttackRange);
        _Reader.getString(_Row, ref _Info.Img);
        _Reader.getString(_Row, ref _Info.Skill1Name);
        _Reader.getString(_Row, ref _Info.Skill2Name);
        _Reader.getInt(_Row, ref _Info.Level);
        _Reader.getInt(_Row, ref _Info.CurExp);
        _Reader.getInt(_Row, ref _Info.MaxExp);
        _Reader.getInt(_Row, ref _Info.UpgrdaeCost);
        _Reader.getInt(_Row, ref _Info.StoreCost);
        _Reader.getString(_Row, ref _Info.MixNeedUnit1);
        _Reader.getString(_Row, ref _Info.MixNeedUnit2);
        _Reader.getString(_Row, ref _Info.MixNeedUnit3);
        _Reader.getString(_Row, ref _Info.Desc);

        return true;
    }
}
