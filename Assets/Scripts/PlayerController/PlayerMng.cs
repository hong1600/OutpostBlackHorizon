using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDmg
{
    void TakeDmg(float _dmg);
}

public class PlayerMng : MonoBehaviour, ITakeDmg
{
    public event Action onTakeDmg;
    public float GetmaxHp() { return maxHp; }
    public float GetcurHp() { return curHp; }

    float maxHp;
    float curHp;
    public bool isDie { get; private set; }

    private void Awake()
    {
        Shared.playerMng = this;

        isDie = false;
        maxHp = 100f;
        curHp = maxHp;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDmg(5);
        }
    }

    public void TakeDmg(float _dmg)
    {
        if (curHp > 0)
        {
            curHp -= _dmg;
            curHp = Mathf.Clamp(curHp, 0, maxHp);
            onTakeDmg?.Invoke();
        }
        if(curHp <= 0) 
        {
            isDie = true;
            StartCoroutine(StartDie());
        }
    }

    IEnumerator StartDie()
    {


        yield return null;
    }
}
