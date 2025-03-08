using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewarder
{
    int GetReward(EReward _eReward);
    void SetReward(EReward _eReward, int _amount);
}

public enum EReward
{
    GOLD,
    GEM,
    PAPER,
    EXP,
}

public class Rewarder : MonoBehaviour, IRewarder
{
    [SerializeField] int rewardGold;
    [SerializeField] int rewardGem;
    [SerializeField] int rewardPaper;
    [SerializeField] int rewardExp;

    private void Awake()
    {
        rewardGold = 0;
        rewardGem = 0;
        rewardPaper = 0;
        rewardExp = 0;
    }

    public int GetReward(EReward _eReward)
    {
        switch (_eReward) 
        {
            case EReward.GOLD:
                return rewardGold;
            case EReward.GEM:
                return rewardGem;
            case EReward.PAPER:
                return rewardPaper;
            case EReward.EXP:
                return rewardExp;
            default:
                return 0;
        }
    }
    public void SetReward(EReward _eReward, int _amount)
    {
        switch (_eReward)
        {
            case EReward.GOLD:
                rewardGold += _amount;
                break;
            case EReward.GEM:
                rewardGem += _amount;
                break;
            case EReward.PAPER:
                rewardPaper += _amount;
                break;
            case EReward.EXP:
                rewardExp += _amount;
                break;
            default:
                break;
        }
    }
}
