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

    [Header("Sync")]
    PhotonView pv;

    protected Vector3 targetPos;
    protected Quaternion targetRot;

    float syncRate = 0.05f;
    float syncTimer = 0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        trail = GetComponent<TrailRenderer>();
        pv = GetComponent<PhotonView>();
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

        if (pv.IsMine)
        {
            rigid.velocity = transform.forward * speed;
        }
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            ReturnPool();
        }

        if (pv.IsMine)
        {
            syncTimer += Time.deltaTime;

            if (syncTimer >= syncRate)
            {
                syncTimer = 0;
                pv.RPC(nameof(RpcMove), RpcTarget.Others, transform.position, transform.rotation);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 20f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 20f);
        }
    }

    [PunRPC]
    protected virtual void RpcMove(Vector3 _pos, Quaternion _rot)
    {
        targetPos = _pos;
        targetRot = _rot;
    }

    protected abstract void ReturnPool();
}
