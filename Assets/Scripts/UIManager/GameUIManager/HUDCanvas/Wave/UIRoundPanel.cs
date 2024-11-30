using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public interface IUIRoundPanel 
{
    void roundPanel();
}

public class UIRoundPanel : MonoBehaviour, IUIRoundPanel
{
    public Round round;
    public IRound iRound;

    public TextMeshProUGUI roundText;


    private void Awake()
    {
        iRound = round;

        roundPanel();
    }

    public void roundPanel()
    {
        roundText.text = $"WAVE {iRound.getCurRound().ToString()}";
    }
}
