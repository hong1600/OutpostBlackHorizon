using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewarder : MonoBehaviour
{
    public GameFlow gameFlow;

    public float rewardGold;
    public float rewardGem;
    public float rewardPaper;
    public float rewardExp;

    public void initialized(GameFlow manager)
    {
        gameFlow = manager;
    }
}
