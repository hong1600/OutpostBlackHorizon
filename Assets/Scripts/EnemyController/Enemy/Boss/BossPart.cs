using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBossPart
{
    BODY,
    LEFT,
    RIGHT,
}

public class BossPart : MonoBehaviour, ITakeDmg
{
    [SerializeField] Robot6 robot6;
    [SerializeField] EBossPart bossPart;

    public void TakeDmg(float _dmg, bool _isHead)
    {
        robot6.TakePartDmg(bossPart, _dmg);
    }
}
