using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalWave;
    [SerializeField] TextMeshProUGUI finalExp;
    [SerializeField] TextMeshProUGUI finalGold;
    [SerializeField] TextMeshProUGUI finalGem;
    [SerializeField] TextMeshProUGUI finalPaper;

    private void main()
    {
        finalWave.text = GameManager.Instance.gameFlow.roundTimer.curRound.ToString();
        finalExp.text = GameManager.Instance.gameFlow.rewarder.rewardExp.ToString();
        finalGold.text = GameManager.Instance.gameFlow.rewarder.rewardGold.ToString();
        finalGem.text = GameManager.Instance.gameFlow.rewarder.rewardGem.ToString();
        finalPaper.text = GameManager.Instance.gameFlow.rewarder.rewardPaper.ToString();
    }
}
