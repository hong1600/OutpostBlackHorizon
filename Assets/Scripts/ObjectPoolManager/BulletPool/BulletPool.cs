using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<EBullet>
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
