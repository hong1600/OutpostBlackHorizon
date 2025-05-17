using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalEnemy : EnemyBase
{
    protected override void Die()
    {
        base.Die();
        goldCoin.SetGold(10);
    }
}
