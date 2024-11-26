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
    }

    private void main()
    {
        myGoldText.text = iGoldCoin.getGold().ToString();
        myCoinText.text = iGoldCoin.getCoin().ToString();
        unitCountText.text = $"{unitMng.curUnitList.Count.ToString()} / 20";
    }
}
