using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    public RoundTimer roundTimer;
    public Rewarder rewarder;

    [SerializeField] TextMeshProUGUI finalWave;
    [SerializeField] TextMeshProUGUI finalExp;
    [SerializeField] TextMeshProUGUI finalGold;
    [SerializeField] TextMeshProUGUI finalGem;
    [SerializeField] TextMeshProUGUI finalPaper;

    private void main()
    {
        finalWave.text = roundTimer.curRound.ToString();
        finalExp.text = rewarder.rewardExp.ToString();
        finalGold.text = rewarder.rewardGold.ToString();
        finalGem.text = rewarder.rewardGem.ToString();
        finalPaper.text = rewarder.rewardPaper.ToString();
    }
}
