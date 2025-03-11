using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGameMoneyPanel : MonoBehaviour
{
    UnitData unitData;

    [SerializeField] TextMeshProUGUI myGoldText;
    [SerializeField] TextMeshProUGUI myCoinText;
    [SerializeField] TextMeshProUGUI unitCountText;

    private void Start()
    {
        unitData = Shared.unitManager.UnitData;

        Shared.gameManager.GoldCoin.onGoldChanged += InGameMoneyPanel;
        Shared.gameManager.GoldCoin.onCoinChanged += InGameMoneyPanel;
        Shared.unitManager.UnitData.onUnitCountEvent += UnitCounterPanel;

        InGameMoneyPanel();
        UnitCounterPanel();
    }

    private void InGameMoneyPanel()
    {
        myGoldText.text = Shared.gameManager.GoldCoin.GetGold().ToString();
        myCoinText.text = Shared.gameManager.GoldCoin.GetCoin().ToString();
    }

    private void UnitCounterPanel()
    {
        unitCountText.text = $"{unitData.GetAllUnitList().Count.ToString()} / 20";
    }
}
