using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoldCoin
{
    event Action onGoldChanged;
    event Action onCoinChanged;
    bool useGold(int amount);
    bool useCoin(int amount);
    int getGold();
    int getCoin();
    void setGold(int amount);
    void setCoin(int amount);
}

public class GoldCoin : MonoBehaviour, IGoldCoin
{
    public event Action onGoldChanged;
    public event Action onCoinChanged;

    public int myGold;
    public int myCoin;

    private void Awake()
    {
        myGold = 2000;
        myCoin = 50;
    }
    public bool useGold(int amount)
    {
        if(myGold >= amount) 
        {
            myGold -= amount;
            onGoldChanged?.Invoke();
            return true;
        }
        return false;
    }

    public bool useCoin(int amount)
    {
        if (myCoin >= amount)
        {
            myCoin -= amount;
            onCoinChanged?.Invoke();
            return true;
        }
        return false;
    }

    public int getGold() { return myGold; }
    public int getCoin() { return myCoin; }
    public void setGold(int amount) { myGold += amount; onGoldChanged?.Invoke(); }
    public void setCoin(int amount) { myCoin += amount; onCoinChanged?.Invoke(); }
}
