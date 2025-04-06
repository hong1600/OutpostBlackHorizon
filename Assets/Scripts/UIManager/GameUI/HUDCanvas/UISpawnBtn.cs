using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpawnBtn : MonoBehaviour
{
    UnitSpawner unitSpawner;
    GoldCoin goldCoin;

    [SerializeField] TextMeshProUGUI spawnGoldText;

    private void Start()
    {
        unitSpawner = UnitManager.instance.UnitSpawner;
        goldCoin = GameManager.instance.GoldCoin;
    }

    public void SpawnBtnText()
    {
        spawnGoldText.text = unitSpawner.GetSpawnGold().ToString();
    }

    public void ClickSpawnBtn()
    {
        if (goldCoin.GetGold() > unitSpawner.GetSpawnGold())
        {
            unitSpawner.SpawnUnit();
        }
    }
}
