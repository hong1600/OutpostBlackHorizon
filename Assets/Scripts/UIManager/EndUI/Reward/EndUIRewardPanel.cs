using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUIRewardPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalWave;
    [SerializeField] TextMeshProUGUI finalExp;
    [SerializeField] TextMeshProUGUI finalGold;
    [SerializeField] TextMeshProUGUI finalGem;
    [SerializeField] TextMeshProUGUI finalPaper;

    private void Main()
    {
        finalWave.text = Shared.gameMng.iRound.GetCurRound().ToString();
        finalGold.text = Shared.gameMng.iRewarder.GetReward(EReward.GOLD).ToString();
        finalGem.text = Shared.gameMng.iRewarder.GetReward(EReward.GEM).ToString();
        finalPaper.text = Shared.gameMng.iRewarder.GetReward(EReward.PAPER).ToString();
        finalExp.text = Shared.gameMng.iRewarder.GetReward(EReward.EXP).ToString();
    }
}
