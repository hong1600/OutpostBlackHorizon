using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SphereCollider sphere;

    [SerializeField] GameObject effect;
    [SerializeField] Transform target;
    [SerializeField] float speed;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
    }

    public void Init(GameObject _enemy)
    {
        target = _enemy.transform;

        if (target == null) return;
        StartCoroutine(StartFire());
    }

    public IEnumerator StartFire()
    {
        while (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance < 1f)
            {
                effect.SetActive(true);
                sphere.enabled = true;

                Collider[] colls = Physics.OverlapSphere(transform.position, sphere.radius, LayerMask.GetMask("Enemy"));

                for (int i = 0; i < colls.Length; i++)
                {
                    Enemy enemy = colls[i].GetComponent<Enemy>();

                    enemy.TakeDamage(100);
                }

                yield return new WaitForSeconds(1);

                Shared.objectPoolMng.ReturnObject(this.gameObject.name, this.gameObject);

                yield break;
            }
            else
            {
                Vector3 targetDir = (target.position - transform.position).normalized;
                this.gameObject.transform.Translate(targetDir * speed * Time.deltaTime);
            }

            yield return null;
        }
    } 
}
