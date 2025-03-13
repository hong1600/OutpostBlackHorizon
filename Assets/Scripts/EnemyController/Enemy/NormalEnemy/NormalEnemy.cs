using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalEnemy : Enemy
{
    protected internal override void Die()
    {
        base.Die();
        goldCoin.SetGold(1);
    }
}
