using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRandomSpawnBtn : MonoBehaviour
{
    public void clickRandomSpwanBtn(int _index)
    {
        Shared.unitManager.UnitRandomSpawner.RandSpawn(_index);
    }
}
