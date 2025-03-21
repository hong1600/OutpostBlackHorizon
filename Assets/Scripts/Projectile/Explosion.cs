using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    SphereCollider sphere;
    EffectPool effectPool;

    float dmg;

    private void Awake()
    {
        effectPool = Shared.objectPoolManager.EffectPool;
        sphere = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Return), 1.5f);
    }

    public void Init(float _dmg)
    {
        dmg = _dmg;
    }

    private void OnTriggerEnter(Collider coll)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, sphere.radius, LayerMask.GetMask("Enemy"));

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

    private void Return()
    {
        effectPool.ReturnEffect(EEffect.ROCKETEXPLOSION, gameObject);
    }
}
