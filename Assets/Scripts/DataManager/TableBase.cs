using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TableBase
{
    string GetTablePath()
    {
#if UNITY_EDITOR
        return Application.dataPath;

#else
        return Application.persistentDataPath + "/Assets";
#endif
    }

    protected void Load_Binary<T>(string _Name, ref T _Obj)
    {
        var b = new BinaryFormatter();

        b.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;

        TextAsset asset = Resources.Load("Data/Table_" + _Name) as TextAsset;

        Stream stream = new MemoryStream(asset.bytes);

        _Obj = (T)b.Deserialize(stream);

        stream.Close();
    }

    protected void Save_Binary(string _Name, object _Obj) 
    {
        string path = GetTablePath() + "/Resources/Data/" + "Table_" + _Name + ".txt";

        var b = new BinaryFormatter();

        Stream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);

        b.Serialize(stream, _Obj);

        stream.Close();
    }

    protected CSVReader GetCSVReader(ETable _eTable) 
    {
        string ext = ".csv";
        string path = "";

        switch (_eTable) 
        {
            case ETable.UNITDATA:
                path = GetTablePath() + "/Resources/Data/Document/Unit/";
                break;
            case ETable.UNITNAME:
                path = GetTablePath() + "/Resources/Data/Document/Unit/";
                break;
            case ETable.UNITSKILL:
                path = GetTablePath() + "/Resources/Data/Document/Unit/";
                break;
            case ETable.ENEMYDATA:
                path = GetTablePath() + "/Resources/Data/Document/Enemy/";
                break;
            case ETable.ITEMDATA:
                path = GetTablePath() + "/Resources/Data/Document/Item/";
                break;
            case ETable.TURRETDATA:
                path = GetTablePath() + "/Resources/Data/Document/Turret/";
                break;
            case ETable.FIELDDATA:
                path = GetTablePath() + "/Resources/Data/Document/Field/";
                break;
            case ETable.MAPDATA:
                path = GetTablePath() + "/Resources/Data/Document/Map/";
                break;
        }

        FileStream file = new FileStream(path + _eTable + ext, FileMode.Open,
            FileAccess.Read, FileShare.ReadWrite);

        StreamReader stream = new StreamReader(file, System.Text.Encoding.UTF8);

        CSVReader reader = new CSVReader();

        reader.parse(stream.ReadToEnd(), false, 1);

        stream.Close();

        return reader;
    }
}
