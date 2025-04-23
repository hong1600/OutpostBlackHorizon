using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    EMissile eMissile;

    SphereCollider sphere;
    EffectPool effectPool;

    float dmg;

    private void Awake()
    {
        effectPool = ObjectPoolManager.instance.EffectPool;
        sphere = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Return), 1.5f);
    }

    public void Init(float _dmg, EMissile _eMissile)
    {
        dmg = _dmg;
        eMissile = _eMissile;
    }

    private void OnTriggerEnter(Collider coll)
    {
        Collider[] hits;

        if (eMissile == EMissile.PLAYER)
        {
            hits = Physics.OverlapSphere
                (transform.position, sphere.radius * transform.localScale.x, LayerMask.GetMask("Enemy"));

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].gameObject.CompareTag("Body"))
                    {
                        ITakeDmg iTakeDmg = hits[i].GetComponentInParent<ITakeDmg>();

                        if (iTakeDmg != null)
                        {
                            iTakeDmg.TakeDmg(dmg, false);
                        }
                    }
                }
            }
        }
        else
        {
            hits = Physics.OverlapSphere
                (transform.position, sphere.radius * transform.localScale.x, LayerMask.GetMask("Player", "Field"));

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    ITakeDmg iTakeDmg = hits[i].GetComponentInParent<ITakeDmg>();

                    if (iTakeDmg != null)
                    {
                        iTakeDmg.TakeDmg(dmg, false);
                    }
                }
            }
        }
    }

    private void Return()
    {
        effectPool.ReturnEffect(EEffect.ROCKETEXPLOSION, gameObject);
    }
}
