using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRandomPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myCoinText;
    private void Start()
    {
    Shared.gameManager.GoldCoin.onCoinChanged -= randomPanel;
    Shared.gameManager.GoldCoin.onCoinChanged += randomPanel;

    myCoinText.text = Shared.gameManager.GoldCoin.GetCoin().ToString();
    }

    private void randomPanel()
    {
        myCoinText.text = Shared.gameManager.GoldCoin.GetCoin().ToString();
    }
}
