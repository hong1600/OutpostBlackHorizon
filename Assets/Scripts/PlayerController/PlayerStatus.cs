using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
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
        curHp = 10;
        maxEnergy = 100;
        curEnergy = 100;
        isDie = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDmg(10);
        }
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
        Shared.gameManager.GameState.SetGameState(EGameState.GAMEOVER);
        yield return null;
    }
}
