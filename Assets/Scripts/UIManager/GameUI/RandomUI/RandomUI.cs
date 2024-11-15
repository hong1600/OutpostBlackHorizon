using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomUI : MonoBehaviour
{
    public TextMeshProUGUI myCoinText;

    private void randomPanel()
    {
        myCoinText.text = GameManager.Instance.myCoin.ToString();
    }
}
