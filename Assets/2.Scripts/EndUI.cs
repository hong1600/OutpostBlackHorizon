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

    private void Update()
    {
        
    }

    private void main()
    {
        finalWave.text = GameManager.Instance.CurRound.ToString();
        finalExp.text = GameManager.Instance.RewardExp.ToString();
        finalGold.text = GameManager.Instance.RewardGold.ToString();
        finalGem.text = GameManager.Instance.RewardGem.ToString();
        finalPaper.text = GameManager.Instance.RewardPaper.ToString();
    }
}
