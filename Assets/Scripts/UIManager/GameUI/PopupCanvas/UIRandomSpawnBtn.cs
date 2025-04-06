using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRandomSpawnBtn : MonoBehaviour
{
    UnitRandomSpawner unitRandomSpawner;

    private void Start()
    {
        unitRandomSpawner = UnitManager.instance.UnitRandomSpawner;
    }

    public void clickRandomSpwanBtn(int _index)
    {
        unitRandomSpawner.RandSpawn(_index);
    }
}
