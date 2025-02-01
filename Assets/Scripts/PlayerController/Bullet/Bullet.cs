using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SphereCollider sphere;

    float speed;
    int damage;
    Transform target;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
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
        float time = 3f;

        while(time >= 0) 
        {
            transform.Translate(Vector3.forward * speed);
            time -= Time.deltaTime;
            yield return null;
        }

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

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null) 
        {
            enemy.TakeDamage(damage);

            Shared.objectPoolMng.ReturnObject(gameObject.name, gameObject);
        }
    }
}
