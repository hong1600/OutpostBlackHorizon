using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineOrbitalTransposer;

public class Bullet : MonoBehaviour
{
    EBullet eBullet;

    Rigidbody rigid;
    SphereCollider sphere;

    ObjectPoolManager objectPool;

    Transform target;
    float speed;
    float dmg;
    float time = 3;

    [SerializeField] TrailRenderer bulletTrail;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        objectPool = Shared.objectPoolManager;
    }

    public void InitBullet(Transform _target, int _dmg, float _speed, 
        EBullet _eBullet)
    {
        time = 3f;

        speed = _speed;
        dmg = _dmg;
        target = _target;
        eBullet = _eBullet;

        if(_eBullet == EBullet.GRENADE) 
        {
            time = 10f;
            rigid.velocity = Vector3.zero;
            Vector3 grenadeDir = transform.up * speed;
            rigid.AddForce(grenadeDir, ForceMode.Impulse);
        }
        else
        {
            Vector3 bulletDir = transform.up * speed;
            rigid.AddForce(bulletDir, ForceMode.Impulse);
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
        if (Physics.Raycast(transform.position, transform.up,
            out RaycastHit hit, speed, ~LayerMask.GetMask("EnemySensor")))
        {
            StartCoroutine(StartRifleBullet(hit.point, hit.collider));
            ReturnPool();
        }
    }

    IEnumerator StartRifleBullet(Vector3 _hitPos, Collider _hitObj)
    {
        GameObject spark = Shared.objectPoolManager.EffectPool.FindEffect(EEffect.GUNHIT);
        spark.transform.position = _hitPos;
        spark.transform.rotation = Quaternion.LookRotation(-transform.forward);

        ITakeDmg iTakeDmg = _hitObj.GetComponentInParent<ITakeDmg>();

        if (_hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy") && iTakeDmg != null)
        {
            bool isHead = _hitObj.CompareTag("Head");
            float finalDmg = isHead ? dmg * 1.5f : dmg;

            iTakeDmg.TakeDmg(finalDmg, isHead);
        }

        yield return null;
    }

    private void MoveGrenadeBullet()
    {
        if (Physics.Raycast(transform.position, transform.forward,
            out RaycastHit hit, sphere.radius, ~LayerMask.GetMask("EnemySensor")))
        {
            StartCoroutine(StartGrenadeBullet(hit.point, hit.collider));
            ReturnPool();
        }
    }

    IEnumerator StartGrenadeBullet(Vector3 _hitPos, Collider _hitObj)
    {
        GameObject plasma = Shared.objectPoolManager.EffectPool.FindEffect(EEffect.PLASMA);
        AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

        plasma.transform.position = _hitPos;
        plasma.transform.rotation = Quaternion.LookRotation(-transform.forward);

        ITakeDmg iTakeDmg = _hitObj.GetComponent<ITakeDmg>();

        if (_hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy") && iTakeDmg != null)
        {
            iTakeDmg.TakeDmg(dmg, false);
        }

        yield return null;
    }

    public void ClearTrail()
    {
        if (bulletTrail != null)
        {
            bulletTrail.Clear();
        }
    }

    private void ReturnPool()
    {
        bulletTrail.Clear();
        objectPool.ReturnObject(gameObject.name, gameObject);
    }
}
