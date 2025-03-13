using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, ITakeDmg
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
        curHp = maxHp;
        maxEnergy = 100;
        curEnergy = 100;
        isDie = false;
    }

    public void TakeDmg(float _dmg, bool _isHead)
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
        Shared.gameManager.GameState.SetGameState(EGameState.GAMEOVER);
        yield return null;
    }

}
