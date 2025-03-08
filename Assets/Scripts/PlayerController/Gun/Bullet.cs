using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;
    EBullet eBullet;
    SphereCollider sphere;
    float speed;
    int damage;
    Transform target;
    float time = 3;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
    }

    public void InitBullet(Transform _target, int _damage, float _speed, EBullet _eBullet, Transform _firePos)
    {
        time = 3f;

        transform.position = _firePos.transform.position;
        transform.rotation = _firePos.transform.rotation * Quaternion.Euler(90, 0, 0);

        speed = _speed;
        damage = _damage;
        target = _target;
        eBullet = _eBullet;

        if(_eBullet == EBullet.GRENADE) 
        {
            time = 10f;
            rigid.velocity = Vector3.zero;
            Vector3 grenadeDir = transform.up * speed;
            rigid.AddForce(grenadeDir, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (eBullet == EBullet.BULLET)
        {
            MoveRifleBullet();
        }

        if (eBullet == EBullet.GRENADE)
        {
            MoveGrenadeBullet();
        }

        if(time < 0) 
        {
            ReturnPool();
        }
    }

    private void MoveRifleBullet()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.up,
            out RaycastHit hit, speed, ~LayerMask.GetMask("EnemySensor")))
        {
            StartCoroutine(StartRifleBullet(hit.point, hit.collider));
        }
    }

    IEnumerator StartRifleBullet(Vector3 _hitPos, Collider _hitObj)
    {
        GameObject spark = Shared.objectPoolManager.EffectPool.FindEffect(EEffect.GUNHIT);

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
        if (Physics.Raycast(transform.position, transform.forward,
            out RaycastHit hit, sphere.radius, ~LayerMask.GetMask("EnemySensor")))
        {
            StartCoroutine(StartGrenadeBullet(hit.point, hit.collider));
        }
    }

    IEnumerator StartGrenadeBullet(Vector3 _hitPos, Collider _hitObj)
    {
        GameObject plasma = Shared.objectPoolManager.EffectPool.FindEffect(EEffect.PLASMA);

        plasma.transform.position = _hitPos;
        plasma.transform.rotation = Quaternion.LookRotation(-transform.forward);

        if (_hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = _hitObj.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }

        yield return null;

        ReturnPool();
    }

    private void ReturnPool()
    {
        Shared.objectPoolManager.ReturnObject(gameObject.name, gameObject);
    }
}
