using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager 
{
    int goldAmount();
    int coinAmount();
    void addGold(int amount);
    void addCoin(int amount);
}

public class GameManager : MonoBehaviour, IGameManager
{
    public static GameManager Instance;

    public GameStateCheck gameStateCheck;

    public int myGold;
    public int myCoin;

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        myGold = 2000;
        myCoin = 50;
    }

    public int goldAmount()
    {
        return myGold;
    }
    public int coinAmount()
    {
        return myCoin;
    }

    public void addGold(int amount)
    {
        myGold += amount;
    }

    public void addCoin(int amount)
    {
        myCoin += amount;
    }
}
