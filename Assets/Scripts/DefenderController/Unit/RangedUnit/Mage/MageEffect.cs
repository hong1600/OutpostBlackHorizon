using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEffect : MonoBehaviour
{
    BoxCollider box;
    EffectPool pool;

    float dmg = 30f;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        pool = ObjectPoolManager.instance.EffectPool;
    }

    private void OnEnable()
    {
        StartCoroutine(StartDamage());
        Invoke("DestroyEffect", 3);
    }

    IEnumerator StartDamage()
    {
        while (true)
        {
            Collider[] colls = Physics.OverlapBox(box.center, box.size * transform.lossyScale.x,
                Quaternion.identity, LayerMask.GetMask("Enemy"));

            for (int i = 0; i < colls.Length; i++)
            {
                ITakeDmg iTakeDmg = colls[i].GetComponent<ITakeDmg>();

                if (iTakeDmg != null)
                {
                    iTakeDmg.TakeDmg(dmg, false);
                }
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void DestroyEffect()
    {
        pool.ReturnEffect(EEffect.MAGE, this.gameObject);
    }
}
