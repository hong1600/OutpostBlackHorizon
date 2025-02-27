using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaveLoader : MonoBehaviour
{
    public string path;

    public DataSaveLoader() 
    {
        path = Application.persistentDataPath + "/save.json";
    }
}
