using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot3 : NormalEnemy
{
    public override void Init(string _name, float _maxHp, float _spd, float _range, float _dmg, EEnemy _eEnemy, int _id)
    {
        base.Init(_name, _maxHp, _spd, _range, _dmg, _eEnemy, _id);
    }
}
