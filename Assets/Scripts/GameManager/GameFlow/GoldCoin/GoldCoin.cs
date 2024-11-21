using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoldCoin
{
    int getGold();
    int getCoin();
    void setGold(int amount);
    void setCoin(int amount);
}

public class GoldCoin : MonoBehaviour, IGoldCoin
{
    public int myGold;
    public int myCoin;

    private void Awake()
    {
        myGold = 2000;
        myCoin = 50;
    }

    public int getGold() { return myGold; }
    public int getCoin() { return myCoin; }
    public void setGold(int amount) { myGold += amount; }
    public void setCoin(int amount) { myCoin += amount; }

}
