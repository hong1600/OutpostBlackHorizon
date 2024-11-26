using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUIRewardPanel : MonoBehaviour
{
    public Round round;
    public IRound iRound;
    public Rewarder rewarder;

    public TextMeshProUGUI finalWave;
    public TextMeshProUGUI finalExp;
    public TextMeshProUGUI finalGold;
    public TextMeshProUGUI finalGem;
    public TextMeshProUGUI finalPaper;

    private void Awake()
    {
        iRound = round;
    }

    private void main()
    {
        finalWave.text = iRound.getCurRound().ToString();
        finalExp.text = rewarder.rewardExp.ToString();
        finalGold.text = rewarder.rewardGold.ToString();
        finalGem.text = rewarder.rewardGem.ToString();
        finalPaper.text = rewarder.rewardPaper.ToString();
    }
}
