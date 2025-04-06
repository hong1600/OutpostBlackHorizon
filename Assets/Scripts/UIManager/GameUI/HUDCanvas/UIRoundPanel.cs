using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoundPanel : MonoBehaviour
{
    Round round;

    public TextMeshProUGUI roundText;

    private void Start()
    {
        round = GameManager.instance.Round;
        round.onRoundEvent += RoundPanel;

        RoundPanel();
    }

    private void RoundPanel()
    {
        if (roundText == null) return;
        roundText.text = $"WAVE {round.curRound.ToString()}";
    }
}
