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
        for (int i = 0; i < 4;  i++) 
        {
            Vector3 offset = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
            Vector3 spawnPos = fireTrs.position + offset;

            GameObject go = pool.FindBullet(EBullet.JETMISSILE, spawnPos, Quaternion.Euler(45, 0, 0));

            JetMissile missile = go.GetComponent<JetMissile>();
            missile.Init(null, 500, 100);
        }
    }
}
