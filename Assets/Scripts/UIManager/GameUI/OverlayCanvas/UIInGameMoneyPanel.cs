using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGameMoneyPanel : MonoBehaviour
{
    UnitData unitData;
    GoldCoin goldCoin;

    [SerializeField] TextMeshProUGUI myGoldText;
    [SerializeField] TextMeshProUGUI myCoinText;
    [SerializeField] TextMeshProUGUI unitCountText;

    private void Start()
    {
        goldCoin = GameManager.instance.GoldCoin;
        unitData = UnitManager.instance.UnitData;

        goldCoin.onGoldChanged += InGameMoneyPanel;
        goldCoin.onCoinChanged += InGameMoneyPanel;
        unitData.onUnitCountEvent += UnitCounterPanel;

        InGameMoneyPanel();
        UnitCounterPanel();
    }

    private void InGameMoneyPanel()
    {
        myGoldText.text = goldCoin.GetGold().ToString();
        myCoinText.text = goldCoin.GetCoin().ToString();
    }

    private void UnitCounterPanel()
    {
        unitCountText.text = $"{unitData.GetAllUnitList().Count.ToString()} / 20";
    }
}
