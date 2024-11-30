using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUIInGameMoneyPanel
{
    void inGameMoneyPanel();
}

public class UIInGameMoneyPanel : MonoBehaviour, IUIInGameMoneyPanel
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
    }

    public void inGameMoneyPanel()
    {
        myGoldText.text = iGoldCoin.getGold().ToString();
        myCoinText.text = iGoldCoin.getCoin().ToString();
        unitCountText.text = $"{unitMng.curUnitList.Count.ToString()} / 20";
    }
}
