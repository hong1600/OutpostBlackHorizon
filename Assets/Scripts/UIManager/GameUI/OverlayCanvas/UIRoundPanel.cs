using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoundPanel : MonoBehaviour
{
    Round round;

    [SerializeField] TextMeshProUGUI roundText;

    private void Start()
    {
        round = GameManager.instance.Round;
        round.onRoundEvent += RoundPanel;

        RoundPanel();
    }

    private void RoundPanel()
    {
        if (round.CurRound == 3)
        {
            roundText.text = $"BOSS";
            return;
        }
        roundText.text = $"WAVE {round.CurRound.ToString()}";
    }
}
