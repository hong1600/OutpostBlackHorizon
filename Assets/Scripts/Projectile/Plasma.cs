using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    SphereCollider sphere;
    EffectPool effectPool;

    HashSet<Collider> targetList = new HashSet<Collider>();

    [SerializeField] int plasmaDmg = 5;
    [SerializeField] float duration = 5f;
    [SerializeField] float dmgInterval = 0.3f;
    float time = 0;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        effectPool = ObjectPoolManager.instance.EffectPool;
    }

    private void OnEnable()
    {
        time = 0f;
        Invoke(nameof(Return), duration);
        StartCoroutine(StartDmgEvent());
    }

    private void OnDisable()
    {
        StopCoroutine(StartDmgEvent());
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void CheckEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, sphere.radius, LayerMask.GetMask("Enemy"));

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].gameObject.CompareTag("Body"))
                {
                    GameObject target = hits[i].gameObject;
                    ITakeDmg iTakeDmg = target.GetComponentInParent<ITakeDmg>();

                    if (iTakeDmg != null)
                    {
                        iTakeDmg.TakeDmg(plasmaDmg, false);
                    }
                }
            }
        }
    }

    IEnumerator StartDmgEvent()
    {
        while (time < duration)
        {
            CheckEnemy();
            yield return new WaitForSeconds(dmgInterval);
        }
    }

    private void Return()
    {
        effectPool.ReturnEffect(EEffect.PLASMA, gameObject);
    }
}
