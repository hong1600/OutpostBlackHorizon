using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletPool
{
    GameObject FindBullet(EBullet _eBullet);
}

public class BulletPool : ObjectPool<EBullet>, IBulletPool
{
    [SerializeField] List<GameObject> bulletList = new List<GameObject>();
    [SerializeField] List<Transform> parentList = new List<Transform>();

    private void Start()
    {
        Init(bulletList, parentList);
    }

    public GameObject FindBullet(EBullet _eBullet)
    {
        return base.FindObject(_eBullet);
    }
}
