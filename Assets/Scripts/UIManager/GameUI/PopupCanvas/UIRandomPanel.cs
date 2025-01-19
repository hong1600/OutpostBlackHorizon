using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRandomPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myCoinText;

    private void Awake()
    {
        Shared.gameMng.iGoldCoin.onCoinChanged -= randomPanel;
        Shared.gameMng.iGoldCoin.onCoinChanged += randomPanel;
    }

    private void Start()
    {
        myCoinText.text = Shared.gameMng.iGoldCoin.GetCoin().ToString();
    }

    private void randomPanel()
    {
        myCoinText.text = Shared.gameMng.iGoldCoin.GetCoin().ToString();
    }
}
