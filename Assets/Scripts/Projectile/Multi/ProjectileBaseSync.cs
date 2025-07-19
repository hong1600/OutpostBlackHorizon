using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProjectileBaseSync : MonoBehaviour
{
    [Header("Components")]
    protected Rigidbody rigid;
    protected SphereCollider sphere;
    protected TrailRenderer trail;

    [Header("Pools")]
    protected EffectPool effectPool;
    protected IBulletPool bulletPool;

    [Header("Hit Effects")]
    protected GameObject hitAim;
    protected Image hitAimImg;

    [Header("Stat")]
    protected Transform target;
    protected bool isHit;
    protected float speed;
    protected float dmg;
    protected float time;
    protected bool isHead;
    protected EBulletType type;

    protected Vector3 targetPos;
    protected Quaternion targetRot;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        bulletPool = Shared.Instance.poolManager.BulletPool;
        effectPool = ObjectPoolManager.instance.EffectPool;

        hitAim = GameUI.instance.HitAim;
        hitAimImg = hitAim.GetComponent<Image>();
    }

    private void OnEnable()
    {
        isHit = false;
        rigid.velocity = Vector3.zero;
    }

    public virtual void Init(Transform _target, float _dmg, float _speed)
    {
        time = 30;

        speed = _speed;
        dmg = _dmg;
        target = _target;

        rigid.velocity = transform.forward * speed;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            ReturnPool();
        }
    }

    protected void HitEnemy(Collider _hitObj)
    {
        ITakeDmg iTakeDmg = _hitObj.GetComponentInParent<ITakeDmg>();

        if (type == EBulletType.PLAYER)
        {
            if (iTakeDmg != null &&
                _hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                isHead = _hitObj.CompareTag("Head");
                float finalDmg = isHead ? dmg * 1.5f : dmg;
                iTakeDmg.TakeDmg(finalDmg, isHead);
            }
        }
        else
        {
            if (iTakeDmg != null && (_hitObj.gameObject.layer == LayerMask.NameToLayer("Player")
                || _hitObj.gameObject.layer == LayerMask.NameToLayer("Field")
                || _hitObj.gameObject.layer == LayerMask.NameToLayer("Center")))
            {
                iTakeDmg.TakeDmg(dmg, false);
            }
        }

    }

    protected abstract void ReturnPool();
}
