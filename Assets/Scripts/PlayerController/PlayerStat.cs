using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public event Action onTakeDmg;

    public float maxHp { get; private set; }
    public float curHp { get; private set; }
    public float maxEnergy { get; private set; }
    public float curEnergy { get; private set; }
    public bool isDie { get; private set; }

    private void Awake()
    {
        maxHp = 100;
        curHp = 100;
        maxEnergy = 100;
        curEnergy = 100;
        isDie = false;
    }

    public void TakeDmg(int _dmg)
    {
        if (curHp > 0)
        {
            curHp -= _dmg;
            curHp = Mathf.Clamp(curHp, 0, maxHp);
            onTakeDmg?.Invoke();
        }
        if (curHp <= 0)
        {
            isDie = true;
            StartCoroutine(StartDie());
        }
    }

    IEnumerator StartDie()
    {
        yield return new WaitForSeconds(1);

        Reset();
    }

    private void Reset()
    {
        isDie = false;
        Shared.playerManager.playerAI.aiState = EPlayer.CREATE;
    }
}
