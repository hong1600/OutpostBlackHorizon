using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SphereCollider sphere;

    Transform target;
    int damage;
    float speed;
    EBullet eBullet;

    [SerializeField] float destroyTime = 3f;
    float time = 0;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    public void InitBullet(Transform _target, int _damage, float _speed, EBullet _eBullet, Transform _firePos)
    {
        time = 0;
        transform.position = _firePos.transform.position;
        transform.rotation = _firePos.transform.rotation * Quaternion.Euler(90, 0, 0);

        speed = _speed;
        damage = _damage;
        target = _target;
        eBullet = _eBullet;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (eBullet == EBullet.BULLET)
        {
            MoveRifleBullet();
        }

        if (eBullet == EBullet.GRENADE)
        {
            MoveGrenadeBullet();
        }

        if(time > destroyTime) 
        {
            ReturnPool();
        }
    }

    private void MoveRifleBullet()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.up,
            out RaycastHit hit, speed * Time.deltaTime, ~LayerMask.GetMask("EnemySensor")))
        {
            StartCoroutine(StartRifleBullet(hit.point, hit.collider));
        }
    }

    IEnumerator StartRifleBullet(Vector3 _hitPos, Collider _hitObj)
    {
        GameObject spark = Shared.objectPoolMng.iEffectPool.FindEffect(EEffect.GUNHIT);

        spark.transform.position = _hitPos;
        spark.transform.rotation = Quaternion.LookRotation(-transform.forward);

        if (_hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = _hitObj.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }

        yield return null;

        ReturnPool();
    }

    private void MoveGrenadeBullet()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.forward,
            out RaycastHit hit, speed * Time.deltaTime, LayerMask.GetMask("Enemy", "Wall")))
        {
            StartCoroutine(StartGrenadeBullet(hit.point, target.position));
        }
    }

    IEnumerator StartGrenadeBullet(Vector3 _hitPos, Vector3 _targetPos)
    {
        yield return null;
    }

    private void ReturnPool()
    {
        Shared.objectPoolMng.ReturnObject(gameObject.name, gameObject);
    }
}
