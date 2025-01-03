using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersLoader : MonoBehaviour
{
    public GameObject dataMng;
    public GameObject SceneMng;
    public GameObject UIMng;

    private void Awake()
    {
        if (GameObject.Find("DataMng") == null)
        {
            Instantiate(dataMng);
        }
        if (GameObject.Find("SceneMng") == null)
        {
            Instantiate(SceneMng);
        }
        if (GameObject.Find("UIMng") == null)
        {
            Instantiate(UIMng);
        }

    }
}
