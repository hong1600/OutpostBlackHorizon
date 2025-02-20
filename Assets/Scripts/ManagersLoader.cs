using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersLoader : MonoBehaviour
{
    [SerializeField] GameObject dataMng;
    [SerializeField] GameObject sceneMng;

    private void Awake()
    {
        if (GameObject.Find("DataMng") == null)
        {
            Instantiate(dataMng);
        }
        if (GameObject.Find("SceneMng") == null)
        {
            Instantiate(sceneMng);
        }
    }
}
