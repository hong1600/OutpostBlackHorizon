using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewarder
{
    public void addRewardGold(int value);
    public void addRewardGem(int value);
    public void addRewardPaper(int value);
    public void addRewardExp(int value);
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
    public void addRewardGold(int value) { rewardGold += value; }
    public void addRewardGem(int value) { rewardGem += value; }
    public void addRewardPaper(int value) { rewardPaper += value; }
    public void addRewardExp(int value) { rewardExp += value; }
}
