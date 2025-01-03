using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewarder
{
    int GetRewardGold();
    int GetRewardGem();
    int GetRewardPaper();
    int GetRewardExp();
    void AddRewardGold(int _value);
    void AddRewardGem(int _value);
    void AddRewardPaper(int _value);
    void AddRewardExp(int _value);
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

    public int GetRewardGold() { return rewardGold; }
    public int GetRewardGem() { return rewardGem; }
    public int GetRewardPaper() { return rewardPaper; }
    public int GetRewardExp() { return rewardExp; }
    public void AddRewardGold(int _value) { rewardGold += _value; }
    public void AddRewardGem(int _value) { rewardGem += _value; }
    public void AddRewardPaper(int _value) { rewardPaper += _value; }
    public void AddRewardExp(int _value) { rewardExp += _value; }
}
