using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SphereCollider sphere;

    float speed;
    int damage;
    Transform target;

    [SerializeField] GameObject explosion;

    private void Awake()
    {
        sphere = explosion.GetComponent<SphereCollider>();
    }

    public void InitBullet(Transform _target, int _damage, float _speed)
    {
        speed = _speed;
        damage = _damage;
        target = _target;

        MoveBullet();
    }

    private void MoveBullet()
    {
        if(target == null) 
        {
            StartCoroutine(StartNonTarget());
        }
        else
        {
            StartCoroutine(StartTarget());
        }
    }

    IEnumerator StartNonTarget()
    {
        float time = 0.75f;

        while(time >= 0)
        {
            transform.Translate(Vector3.forward * speed);
            time -= Time.deltaTime;
            yield return null;
        }

        explosion.SetActive(true);

        Collider[] colls = Physics.OverlapSphere
            (explosion.transform.position, sphere.radius, LayerMask.GetMask("Enemy"));

        for(int i = 0; i < colls.Length; i++) 
        {
            Enemy enemy = colls[i].gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }

        yield return new WaitForSeconds(0.5f);

        explosion.SetActive(false);
        Shared.objectPoolMng.ReturnObject(gameObject.name, gameObject);
    }

    IEnumerator StartTarget()
    {
        while (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                Shared.objectPoolMng.ReturnObject(gameObject.name, gameObject);
                yield break;
            }

            yield return null;
        }

        Shared.objectPoolMng.ReturnObject(gameObject.name, gameObject);
    }
}
