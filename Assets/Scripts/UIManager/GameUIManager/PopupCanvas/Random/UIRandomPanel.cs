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
    }

    private void randomPanel()
    {
        myCoinText.text = iGoldCoin.getCoin().ToString();
    }
}
