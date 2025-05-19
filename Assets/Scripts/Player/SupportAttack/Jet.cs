using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    BulletPool pool;

    [SerializeField] Transform fireTrs;

    private void Start()
    {
        pool = ObjectPoolManager.instance.BulletPool;
    }

    public void Drop()
    {
        GameObject go = pool.FindBullet(EBullet.JETGREANDE, fireTrs.position, Quaternion.Euler(50, -90, 0));
        JetGrenade grenade = go.GetComponent<JetGrenade>();
        grenade.Init(null, 500, 100);
    }
}
