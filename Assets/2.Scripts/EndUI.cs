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
        finalWave.text = GameManager.Instance.gameFlow.curRound.ToString();
        finalExp.text = GameManager.Instance.rewardGameOverMng.rewardExp.ToString();
        finalGold.text = GameManager.Instance.rewardGameOverMng.rewardGold.ToString();
        finalGem.text = GameManager.Instance.rewardGameOverMng.rewardGem.ToString();
        finalPaper.text = GameManager.Instance.rewardGameOverMng.rewardPaper.ToString();
    }
}
