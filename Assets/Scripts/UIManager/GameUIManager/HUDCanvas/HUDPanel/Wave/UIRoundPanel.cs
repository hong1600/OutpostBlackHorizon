using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIRoundPanel : MonoBehaviour
{
    public Round round;
    public IRound iRound;

    public TextMeshProUGUI roundText;


    private void Awake()
    {
        iRound = round;
    }

    public void roundPanel()
    {
        roundText.text = $"WAVE {iRound.getCurRound().ToString()}";
    }
}
