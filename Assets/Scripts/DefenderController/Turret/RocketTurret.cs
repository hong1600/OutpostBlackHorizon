using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : Defender
{
    TableTurret.Info info;

    [SerializeField] Transform[] fireTrs;
    [SerializeField] float bulletSpd;

    private void Start()
    {
        info = DataManager.instance.TableTurret.GetTurretData(402);
        base.Init(info.ID, info.Name, info.Damage, info.AttackSpeed, info.AttackRange, info.ImgPath, true);
    }

    protected override IEnumerator OnDamageEvent(Enemy _enemy, int _dmg)
    {
        for (int i = 0; i < fireTrs.Length; i++)
        {
            GameObject missileObj = bulletPool.FindBullet(EBullet.ROCKETMISSILE, fireTrs[i].position, fireTrs[i].rotation);
            RocketMissile missile = missileObj.GetComponent<RocketMissile>();
            missile.Init(target.transform, attackDamage, bulletSpd);

            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    protected internal override void LookTarget()
    {
        if (target != null)
        {
            if (targetColls == null)
            {
                targetColls = target.GetComponentsInChildren<Collider>();

                for (int i = 0; i < targetColls.Length; i++)
                {
                    if (targetColls[i].gameObject.CompareTag("Body"))
                    {
                        targetHeight = targetColls[i].bounds.center.y;
                        break;
                    }
                }
            }

            Vector3 targetVelocity = enemy.GetComponent<Rigidbody>().velocity;
            Vector3 predictionPos = new Vector3
                (target.transform.position.x, targetHeight, target.transform.position.z) + targetVelocity * 2f;
            Vector3 dir = (predictionPos - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(dir) * Quaternion.Euler(-40f, 0f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
