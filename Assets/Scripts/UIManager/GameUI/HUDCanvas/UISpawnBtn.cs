using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpawnBtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spawnGoldText;

    public void SpawnBtnText()
    {
        spawnGoldText.text = Shared.unitManager.UnitSpawner.GetSpawnGold().ToString();
    }

    public void ClickSpawnBtn()
    {
        if (Shared.gameManager.GoldCoin.GetGold() > Shared.unitManager.UnitSpawner.GetSpawnGold())
        {
            Shared.unitManager.UnitSpawner.SpawnUnit();
        }
    }
}
