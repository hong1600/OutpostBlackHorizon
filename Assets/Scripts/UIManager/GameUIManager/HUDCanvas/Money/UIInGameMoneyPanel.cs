using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGameMoneyPanel : MonoBehaviour
{
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;
    public UnitMng unitMng;
    public IUnitMng iUnitMng;

    public TextMeshProUGUI myGoldText;
    public TextMeshProUGUI myCoinText;
    public TextMeshProUGUI unitCountText;


    private void Awake()
    {
        iGoldCoin = goldCoin;
        iUnitMng = unitMng;

        iGoldCoin.onGoldChanged += inGameMoneyPanel;
        iGoldCoin.onCoinChanged += inGameMoneyPanel;
        iUnitMng.onUnitCountChange += unitCounterPanel;
    }

    private void Start()
    {
        inGameMoneyPanel();
        unitCounterPanel();
    }

    public void inGameMoneyPanel()
    {
        myGoldText.text = iGoldCoin.getGold().ToString();
        myCoinText.text = iGoldCoin.getCoin().ToString();
    }

    public void unitCounterPanel()
    {
        unitCountText.text = $"{iUnitMng.getCurUnitList().Count.ToString()} / 20";
    }
}
