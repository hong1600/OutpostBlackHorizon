using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class UnitUpgrader : MonoBehaviour
{
    public void unitUpgradeCost(ref int cost, int amount, string type = "Gold")
    {
        if (type == "Gold")
            iGoldCoin.setGold(-cost);
        else if (type == "Coin")
            iGoldCoin.setCoin(-cost);

        cost += amount;
    }
}
