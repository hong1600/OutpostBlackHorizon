using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MageEffect : MonoBehaviour
{
    BoxCollider box;
    float dmg = 30f;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(StartDamage());
        Invoke("DestroyEffect", 3);
    }

    IEnumerator StartDamage()
    {
        while (true)
        {
            Collider[] colls = Physics.OverlapBox(box.center, box.size,
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
        Shared.objectPoolManager.ReturnObject(this.gameObject.name, this.gameObject);
    }
}
