using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewarder
{
    public void setRewardGold(int value);
    public void setRewardGem(int value);
    public void setRewardPaper(int value);
    public void setRewardExp(int value);
}

public class Rewarder : MonoBehaviour, IRewarder
{
    public int rewardGold;
    public int rewardGem;
    public int rewardPaper;
    public int rewardExp;

    private void Awake()
    {
        rewardGold = 0;
        rewardGem = 0;
        rewardPaper = 0;
        rewardExp = 0;
    }
    public void setRewardGold(int value) { rewardGold += value; }
    public void setRewardGem(int value) { rewardGem += value; }
    public void setRewardPaper(int value) { rewardPaper += value; }
    public void setRewardExp(int value) { rewardExp += value; }
}
