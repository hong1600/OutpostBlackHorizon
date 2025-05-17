using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProjectileBase : MonoBehaviour
{
    [Header("Components")]
    protected Rigidbody rigid;
    protected SphereCollider sphere;
    protected TrailRenderer trail;

    [Header("Pools")]
    protected EffectPool effectPool;
    protected BulletPool bulletPool;

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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        bulletPool = ObjectPoolManager.instance.BulletPool;
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

        Vector3 bulletDir = transform.forward * speed;
        rigid.velocity = bulletDir;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            ReturnPool();
        }
    }

    protected abstract void ReturnPool();
}
