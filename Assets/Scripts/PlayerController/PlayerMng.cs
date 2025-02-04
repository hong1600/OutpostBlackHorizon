using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public Player player;

    public event Action onTakeDmg;

    public float maxHp { get; private set; } = 100f;
    public float curHp { get; private set; } = 100f;

    private void Awake()
    {
        Shared.playerMng = this;
        player = this.gameObject.GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) 
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
            player.isDie = true;
            StartCoroutine(player.StartDie());
        }
    }
}
