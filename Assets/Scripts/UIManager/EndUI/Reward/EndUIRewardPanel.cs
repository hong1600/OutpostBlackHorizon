using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUIRewardPanel : MonoBehaviour
{
    public TextMeshProUGUI finalWave;
    public TextMeshProUGUI finalExp;
    public TextMeshProUGUI finalGold;
    public TextMeshProUGUI finalGem;
    public TextMeshProUGUI finalPaper;

    private void Main()
    {
        finalWave.text = Shared.gameMng.iRound.GetCurRound().ToString();
        finalGold.text = Shared.gameMng.iRewarder.GetRewardGold().ToString();
        finalGem.text = Shared.gameMng.iRewarder.GetRewardGem().ToString();
        finalPaper.text = Shared.gameMng.iRewarder.GetRewardPaper().ToString();
        finalExp.text = Shared.gameMng.iRewarder.GetRewardPaper().ToString();
    }
}
