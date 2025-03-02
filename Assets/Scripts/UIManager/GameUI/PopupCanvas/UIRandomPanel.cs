using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRandomPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myCoinText;
    private void Start()
    {
    Shared.gameMng.GoldCoin.onCoinChanged -= randomPanel;
    Shared.gameMng.GoldCoin.onCoinChanged += randomPanel;

    myCoinText.text = Shared.gameMng.GoldCoin.GetCoin().ToString();
    }

    private void randomPanel()
    {
        myCoinText.text = Shared.gameMng.GoldCoin.GetCoin().ToString();
    }
}
