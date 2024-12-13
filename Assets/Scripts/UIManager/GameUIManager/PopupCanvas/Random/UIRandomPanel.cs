using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRandomPanel : MonoBehaviour
{
    public GoldCoin goldCoin;
    public IGoldCoin iGoldCoin;

    public TextMeshProUGUI myCoinText;

    private void Awake()
    {
        iGoldCoin = goldCoin;

        iGoldCoin.onCoinChanged += randomPanel;
    }

    private void Start()
    {
        myCoinText.text = iGoldCoin.getCoin().ToString();
    }

    public void randomPanel()
    {
        myCoinText.text = iGoldCoin.getCoin().ToString();
    }
}
