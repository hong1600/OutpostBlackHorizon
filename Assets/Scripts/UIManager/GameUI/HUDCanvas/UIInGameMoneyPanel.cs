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
        Shared.gameMng.iGoldCoin.onGoldChanged += InGameMoneyPanel;
        Shared.gameMng.iGoldCoin.onCoinChanged += InGameMoneyPanel;
        Shared.unitMng.onUnitCountEvent += UnitCounterPanel;

        InGameMoneyPanel();
        UnitCounterPanel();
    }

    private void InGameMoneyPanel()
    {
        myGoldText.text = Shared.gameMng.iGoldCoin.GetGold().ToString();
        myCoinText.text = Shared.gameMng.iGoldCoin.GetCoin().ToString();
    }

    private void UnitCounterPanel()
    {
        unitCountText.text = $"{Shared.unitMng.GetAllUnitList().Count.ToString()} / 20";
    }
}
