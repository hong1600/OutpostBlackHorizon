using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name = "";
    public int level = 1;
    public float curExp = 0f;
    public float maxExp = 100f;
    public float gold = 0;
    public int gem = 0;
    public int paper = 0;
    public bool first = true;

    public List<TreasureData> items = null;
    public List<UnitData> units = null;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData playerdata = new PlayerData();

    public string path;

    public List<TreasureData> item;
    public List<UnitData> unit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";
    }

    public void saveData()
    {
        string data = JsonUtility.ToJson(playerdata);
        File.WriteAllText(path, data);
    }

    public void loadData()
    {
        string data = File.ReadAllText(path);
        playerdata = JsonUtility.FromJson<PlayerData>(data);
        playerdata.items = item;
        playerdata.units = unit;
    }
}
