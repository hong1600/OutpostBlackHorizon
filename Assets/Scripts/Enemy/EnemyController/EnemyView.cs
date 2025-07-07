using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyView : MonoBehaviour
{
    Animator anim;
    [SerializeField] BoxCollider box;

    [Header("HitEffect")]
    public Material hitMat;
    public Renderer render;
    public Material originMat;

    [Header("HpBar")]
    HpBarPool hpBarPool;
    public SkinnedMeshRenderer skinRender { get; private set; }
    public EnemyHpBar enemyHpBar { get; private set; }

    [Header("Effect")]
    EffectPool effectPool;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        skinRender = GetComponentInChildren<SkinnedMeshRenderer>();

        if (render != null)
        {
            originMat = render.sharedMaterial;
        }
    }

    public void CreateHpBar(EnemyBase _enemyBase)
    {
        if (hpBarPool == null)
        {
            hpBarPool = ObjectPoolManager.instance.HpBarPool;
        }

        if (skinRender != null)
        {
            GameObject hpBar = hpBarPool.FindHpbar(EHpBar.NORMAL, skinRender.bounds.center +
                new Vector3(0, skinRender.bounds.extents.y + 0.5f, 0), Quaternion.identity);

            enemyHpBar = hpBar.GetComponent<EnemyHpBar>();

            enemyHpBar.Init(_enemyBase, skinRender);
        }
    }

    public void InitHpBar(EnemyBase _enemyBase)
    {
        if (hpBarPool == null)
        {
            hpBarPool = ObjectPoolManager.instance.HpBarPool;
        }

        if (skinRender != null)
        {
            if (enemyHpBar != null)
            {
                hpBarPool.ReturnHpBar(EHpBar.NORMAL, enemyHpBar.gameObject);
                enemyHpBar = null;
            }

            GameObject hpBar = hpBarPool.FindHpbar(EHpBar.NORMAL, skinRender.bounds.center +
                new Vector3(0, skinRender.bounds.extents.y + 0.5f, 0), Quaternion.identity);

            enemyHpBar = hpBar.GetComponent<EnemyHpBar>();
            enemyHpBar.Init(_enemyBase, skinRender);
        }

    }

    public void ReturnHpBar()
    {
        if (enemyHpBar != null)
        {
            hpBarPool.ReturnHpBar(EHpBar.NORMAL, enemyHpBar.gameObject);
        }
    }

    public GameObject PlayDieEffect()
    {
        if (effectPool == null)
        {
            effectPool = ObjectPoolManager.instance.EffectPool;
        }

        GameObject explosionObj =
            effectPool.FindEffect(EEffect.ENEMYEXPLOSION, box.bounds.center, Quaternion.identity);

        return explosionObj;
    }

    public void ChangeHitMat()
    {
        if (render != null && hitMat != null)
            render.sharedMaterial = hitMat;
    }

    public void ResetMat()
    {
        if (render != null && hitMat != null)
            render.material = originMat;
    }

    public void ChangeAnim(EEnemyAI _curState)
    {
        switch (_curState)
        {
            case EEnemyAI.CREATE:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.IDLE);
                break;
            case EEnemyAI.MOVE:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.WALK);
                break;
            case EEnemyAI.ATTACK:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.ATTACK);
                break;
            case EEnemyAI.STAY:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.IDLE);
                break;
            case EEnemyAI.DIE:
                anim.SetInteger("EnemyAnim", (int)EEnemyAnim.DIE);
                break;
        }
    }

}
