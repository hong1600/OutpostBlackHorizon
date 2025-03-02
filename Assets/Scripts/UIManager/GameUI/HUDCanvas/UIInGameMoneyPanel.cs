using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGameMoneyPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myGoldText;
    [SerializeField] TextMeshProUGUI myCoinText;
    [SerializeField] TextMeshProUGUI unitCountText;

    private void Start()
    {
        Shared.gameMng.GoldCoin.onGoldChanged += InGameMoneyPanel;
        Shared.gameMng.GoldCoin.onCoinChanged += InGameMoneyPanel;
        Shared.unitMng.onUnitCountEvent += UnitCounterPanel;

        InGameMoneyPanel();
        UnitCounterPanel();
    }

    private void InGameMoneyPanel()
    {
        myGoldText.text = Shared.gameMng.GoldCoin.GetGold().ToString();
        myCoinText.text = Shared.gameMng.GoldCoin.GetCoin().ToString();
    }

    private void UnitCounterPanel()
    {
        unitCountText.text = $"{Shared.unitMng.GetAllUnitList().Count.ToString()} / 20";
    }
}
