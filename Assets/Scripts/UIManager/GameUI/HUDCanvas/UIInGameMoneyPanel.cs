using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGameMoneyPanel : MonoBehaviour
{
    public TextMeshProUGUI myGoldText;
    public TextMeshProUGUI myCoinText;
    public TextMeshProUGUI unitCountText;

    private void Start()
    {
        Shared.gameMng.iGoldCoin.onGoldChanged += InGameMoneyPanel;
        Shared.gameMng.iGoldCoin.onCoinChanged += InGameMoneyPanel;
        Shared.unitMng.onUnitCountChange += UnitCounterPanel;

        InGameMoneyPanel();
        UnitCounterPanel();

    }

    public void InGameMoneyPanel()
    {
        myGoldText.text = Shared.gameMng.iGoldCoin.GetGold().ToString();
        myCoinText.text = Shared.gameMng.iGoldCoin.GetCoin().ToString();
    }

    public void UnitCounterPanel()
    {
        unitCountText.text = $"{Shared.unitMng.GetAllUnitList().Count.ToString()} / 20";
    }
}
