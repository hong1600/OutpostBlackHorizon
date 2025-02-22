using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    TableMgr tableMgr;

    private void Start()
    {
        tableMgr = new TableMgr();
    }
}
