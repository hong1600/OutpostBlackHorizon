using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpawnBtn : MonoBehaviour
{
    public UnitSpawner unitSpawner;
    public IUnitSpawner iUnitSpawner;
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;

    public TextMeshProUGUI spawnGoldText;


    private void Awake()
    {
        iUnitSpawner = unitSpawner;
        iGoldCoin = goldCoin;
    }

    public void spawnBtnText()
    {
        spawnGoldText.text = iUnitSpawner.getSpawnGold().ToString();
    }

    public void clickSpawnBtn()
    {
        if (iGoldCoin.getGold() > iUnitSpawner.getSpawnGold())
        {
            iUnitSpawner.spawnUnit();
        }
    }
}
