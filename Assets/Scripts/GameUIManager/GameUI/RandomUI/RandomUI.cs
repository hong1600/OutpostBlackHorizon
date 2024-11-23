using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomUI : MonoBehaviour
{
    public IGoldCoin iGoldCoin;

    public TextMeshProUGUI myCoinText;

    public void initialized(IGoldCoin iGoldCoin)
    {
        this.iGoldCoin = iGoldCoin;
    }

    private void randomPanel()
    {
        myCoinText.text = iGoldCoin.getCoin().ToString();
    }
}
