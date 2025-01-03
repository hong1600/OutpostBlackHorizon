using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpawnBtn : MonoBehaviour
{
    public TextMeshProUGUI spawnGoldText;

    public void SpawnBtnText()
    {
        spawnGoldText.text = Shared.unitMng.iUnitSpawner.GetSpawnGold().ToString();
    }

    public void ClickSpawnBtn()
    {
        if (Shared.gameMng.iGoldCoin.GetGold() > Shared.unitMng.iUnitSpawner.GetSpawnGold())
        {
            Shared.unitMng.iUnitSpawner.SpawnUnit();
        }
    }
}
