using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDmg
{
    void TakeDmg(float _dmg, bool _isHead);
}

public interface ICutScene
{
    void Play();
}

public interface IObjectPoolManager
{
    IBulletPool BulletPool { get; }
    IEnemyPool EnemyPool { get; }
}

public interface IBulletPool
{
    GameObject FindBullet(EBullet _type, Vector3 _pos, Quaternion _rot);
    void ReturnBullet(EBullet _type, GameObject obj);
}

public interface IEnemyPool
{
    GameObject FindEnemy(EEnemy _type, Vector3 _pos, Quaternion _rot);
    void ReturnEnemy(EEnemy _type, GameObject obj);
    Transform ParentDic(EEnemy _type);
    void AddEvent(Action _handler);
}
