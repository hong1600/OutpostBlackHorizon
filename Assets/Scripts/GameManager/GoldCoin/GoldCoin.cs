using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoldCoin
{
    event Action onGoldChanged;
    event Action onCoinChanged;
    bool UseGold(int _amount);
    bool UseCoin(int _amount);
    int GetGold();
    int GetCoin();
    void SetGold(int _amount);
    void SetCoin(int _amount);
}

public class GoldCoin : MonoBehaviour, IGoldCoin
{
    public event Action onGoldChanged;
    public event Action onCoinChanged;

    [SerializeField] int myGold;
    [SerializeField] int myCoin;

    private void Awake()
    {
        myGold = 5000;
        myCoin = 10;
    }

    public bool UseGold(int _amount)
    {
        if(myGold >= _amount) 
        {
            myGold -= _amount;
            onGoldChanged?.Invoke();
            return true;
        }
        return false;
    }

    public bool UseCoin(int _amount)
    {
        if (myCoin >= _amount)
        {
            myCoin -= _amount;
            onCoinChanged?.Invoke();
            return true;
        }
        return false;
    }

    public int GetGold() { return myGold; }
    public int GetCoin() { return myCoin; }
    public void SetGold(int _amount) { myGold += _amount; onGoldChanged?.Invoke(); }
    public void SetCoin(int _amount) { myCoin += _amount; onCoinChanged?.Invoke(); }
}
