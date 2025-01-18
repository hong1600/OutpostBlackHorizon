using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    BoxCollider box;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(StartDamage());
        Invoke("DestroyEffect", 3);
    }

    //void OnTriggerStay(Collider coll)
    //{
    //    if (coll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        Enemy enemy = coll.GetComponent<Enemy>();

    //        if (enemy != null) 
    //        {
    //            enemy.TakeDamage(30);
    //        }
    //    }
    //}

    IEnumerator StartDamage()
    {
        while (true)
        {
            Collider[] colls = Physics.OverlapBox(box.center, box.size,
                Quaternion.identity, LayerMask.GetMask("Enemy"));

            for (int i = 0; i < colls.Length; i++)
            {
                Enemy enemy = colls[i].GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(30);
                }
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void DestroyEffect()
    {
        Shared.objectPoolMng.ReturnObject(this.gameObject.name, this.gameObject);
    }
}
