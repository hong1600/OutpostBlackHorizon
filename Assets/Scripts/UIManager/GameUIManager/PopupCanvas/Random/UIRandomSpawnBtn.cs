using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRandomSpawnBtn : MonoBehaviour
{
    public UnitRandomSpawner unitRandomSpawner;
    public IUnitRandomSpawner iUnitRandomSpawner;

    private void Awake()
    {
        iUnitRandomSpawner = unitRandomSpawner;
    }

    public void clickRandomSpwanBtn(int index)
    {
        iUnitRandomSpawner.randSpawn(index);
    }
}
