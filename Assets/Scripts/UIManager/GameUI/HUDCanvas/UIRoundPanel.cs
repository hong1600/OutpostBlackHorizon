using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoundPanel : MonoBehaviour
{
    public TextMeshProUGUI roundText;

    private void Start()
    {
        RoundPanel();
        Shared.gameManager.Round.onRoundEvent += RoundPanel;
    }

    private void RoundPanel()
    {
        if (roundText == null) return;
        roundText.text = $"WAVE {Shared.gameManager.Round.curRound.ToString()}";
    }
}
