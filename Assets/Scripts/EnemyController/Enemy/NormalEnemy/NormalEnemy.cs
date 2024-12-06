using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalEnemy : Enemy
{
    public override void die()
    {
        base.die();
        iGoldCoin.setGold(1);
    }
}
