using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MeleeUnit
{
    [SerializeField] UnitData unitData;

    private void Awake()
    {
        Init(unitData);
    }
    protected override IEnumerator OnDamageEvent(Enemy enemy)
    {
        int rand = Random.Range(0, 100);

        if (rand < 12)
        {
            enemy.enemyAI.aiState = EEnemyAI.STAY;
        }

        yield return null;
    }
}
