using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRandomPanel : MonoBehaviour
{
    public TextMeshProUGUI myCoinText;

    private void Awake()
    {
        Shared.gameMng.iGoldCoin.onCoinChanged -= randomPanel;
        Shared.gameMng.iGoldCoin.onCoinChanged += randomPanel;
    }

    private void Start()
    {
        myCoinText.text = Shared.gameMng.iGoldCoin.GetCoin().ToString();
    }

    public void randomPanel()
    {
        myCoinText.text = Shared.gameMng.iGoldCoin.GetCoin().ToString();
    }
}
