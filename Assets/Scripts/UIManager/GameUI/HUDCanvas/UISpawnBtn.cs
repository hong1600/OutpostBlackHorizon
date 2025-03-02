using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpawnBtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spawnGoldText;

    public void SpawnBtnText()
    {
        spawnGoldText.text = Shared.unitMng.iUnitSpawner.GetSpawnGold().ToString();
    }

    public void ClickSpawnBtn()
    {
        if (Shared.gameMng.GoldCoin.GetGold() > Shared.unitMng.iUnitSpawner.GetSpawnGold())
        {
            Shared.unitMng.iUnitSpawner.SpawnUnit();
        }
    }
}
