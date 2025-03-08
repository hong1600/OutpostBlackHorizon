using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunslingerEffect : MonoBehaviour
{
    SphereCollider sphere;

    [SerializeField] GameObject effect;
    float speed;
    int damage;
    Transform target;

    public void Init(Transform _target, int _damage, float _speed)
    {
        sphere = GetComponent<SphereCollider>();

        target = _target;
        damage = _damage;
        speed = _speed;

        StartCoroutine(StartFire());
    }

    private IEnumerator StartFire()
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

                Shared.objectPoolManager.ReturnObject(this.gameObject.name, this.gameObject);

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
