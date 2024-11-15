using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewarder : MonoBehaviour
{
    public GameFlow gameFlow;

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
}
