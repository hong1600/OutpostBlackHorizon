using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUIRoundPanel 
{
    void RoundPanel();
}

public class UIRoundPanel : MonoBehaviour, IUIRoundPanel
{
    public TextMeshProUGUI roundText;

    private void Start()
    {
        RoundPanel();
    }

    public void RoundPanel()
    {
        if (roundText == null) return;
        roundText.text = $"WAVE {Shared.gameMng.iRound.GetCurRound().ToString()}";
    }
}
