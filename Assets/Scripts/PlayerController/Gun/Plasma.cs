using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    SphereCollider sphere;

    [SerializeField] int plasmaDmg = 5;
    [SerializeField] float duration = 5f;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        Invoke("Return", duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            StartCoroutine(StartDmg(other));
        }
    }

    IEnumerator StartDmg(Collider _target)
    {
        float time = 0;

        while (time < duration) 
        {
            time += Time.deltaTime;

            if (_target != null) 
            {
                ITakeDmg iTakeDmg = _target.GetComponent<ITakeDmg>();

                if(iTakeDmg != null) 
                {
                    iTakeDmg.TakeDmg(plasmaDmg);
                }
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Return()
    {
        Shared.objectPoolManager.ReturnObject(this.gameObject.name, gameObject);
    }
}
