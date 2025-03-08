using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    SphereCollider sphere;

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

    IEnumerator StartDmg(Collider _enemy)
    {
        float time = 0;

        while (time < duration) 
        {
            time += Time.deltaTime;

            if (_enemy != null) 
            {
                Enemy enemy = _enemy.GetComponent<Enemy>();
                if(enemy != null) 
                {
                    enemy.TakeDamage(5);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void Return()
    {
        Shared.objectPoolManager.ReturnObject(this.gameObject.name, gameObject);
    }
}
