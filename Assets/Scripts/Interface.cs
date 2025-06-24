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
}

public interface IBulletPool
{
    GameObject FindBullet(EBullet _type, Vector3 _pos, Quaternion _rot);
    void ReturnBullet(EBullet _type, GameObject obj);
}
