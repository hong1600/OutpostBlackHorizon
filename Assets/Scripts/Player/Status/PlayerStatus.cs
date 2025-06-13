using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, ITakeDmg
{
    public event Action onTakeDmg;
    public event Action onFillHp;
    public event Action onUseEnergy;

    float hptimer;

    bool isTakeDmg = false;

    public float maxHp { get; private set; } = 100f;
    public float curHp { get; private set; } = 100;
    public float maxEnergy { get; private set; } = 100f;
    public float curEnergy { get; private set; } = 100f;
    public bool isDie { get; private set; } = false;

    private void Update()
    {
        FillHp(5);

        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDmg(100f, false);
        }
    }


    public void TakeDmg(float _dmg, bool _isHead)
    {
        if (curHp > 0)
        {
            curHp -= _dmg;
            curHp = Mathf.Clamp(curHp, 0, maxHp);
            isTakeDmg = true;
            hptimer = 0;
            onTakeDmg?.Invoke();
        }
        if (curHp <= 0)
        {
            isDie = true;
            StartCoroutine(StartDie());
        }
    }

    private void FillHp(float _hp)
    {
        if (isTakeDmg)
        {
            hptimer += Time.deltaTime;

            if (hptimer > 5)
            {
                isTakeDmg = false;
            }
        }
        else
        {
            curHp += _hp * Time.deltaTime;
            curHp = Mathf.Clamp(curHp, 0, maxHp);
            onFillHp?.Invoke();
        }
    }

    IEnumerator StartFillHp()
    {
        yield return null;
    }

    public void UseEnergy(float _energy)
    {
        curEnergy -= _energy;
        onUseEnergy?.Invoke();
    }

    public void FillEnergy(float _energy)
    {
        curEnergy += _energy;
        onUseEnergy?.Invoke();
    }

    IEnumerator StartDie()
    {
        PlayerManager.instance.Rifle.SetActive(false);
        CameraManager.instance.CameraFpsDead.MoveCam();
        GameManager.instance.GameState.SetGameState(EGameState.GAMEOVER);
        yield return null;
    }
}
