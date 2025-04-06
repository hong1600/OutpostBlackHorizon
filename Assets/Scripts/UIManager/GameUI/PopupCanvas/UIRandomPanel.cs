using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRandomPanel : MonoBehaviour
{
    GoldCoin goldCoin;

    [SerializeField] TextMeshProUGUI myCoinText;

    private void Start()
    {
        goldCoin = GameManager.instance.GoldCoin;

        goldCoin.onCoinChanged -= randomPanel;
        goldCoin.onCoinChanged += randomPanel;

        myCoinText.text = goldCoin.GetCoin().ToString();
    }

    private void randomPanel()
    {
        myCoinText.text = goldCoin.GetCoin().ToString();
    }
}
