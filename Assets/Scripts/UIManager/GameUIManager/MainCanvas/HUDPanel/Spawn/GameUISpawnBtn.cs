using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUISpawnBtn : MonoBehaviour
{
    public UnitSpawner unitSpawner;
    public IUnitSpawner iUnitSpanwer;

    public TextMeshProUGUI spawnGoldText;


    private void Awake()
    {
        iUnitSpanwer = unitSpawner;
    }

    public void spawnBtn()
    {
        spawnGoldText.text = unitSpawner.spawnGold.ToString();
    }

    public void clickSpawnBtn()
    {

    }
}
