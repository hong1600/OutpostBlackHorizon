using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot6 : Boss
{
    TableEnemy.Info info;

    [SerializeField] BoxCollider leftHandBox;
    [SerializeField] BoxCollider rightHandBox;

    [SerializeField] GameObject leftHandEffect;
    [SerializeField] GameObject rightHandEffect;

    bool isLeftHand = true;
    bool isRightHand = true;

    [Header("Missile")]
    [SerializeField] float speed = 50;
    [SerializeField] float spacing = 3f;
    [SerializeField] Transform leftMissileTrs;
    [SerializeField] Transform rightMissileTrs;
    Vector3[] fireTrsOffset;

    [Header("Laser")]
    [SerializeField] Transform laserFireTrs;
    [SerializeField] GameObject chargeLaserEffect;
    [SerializeField] LineRenderer chargeLaserLine;
    [SerializeField] GameObject laserEffect;

    int pattonNum;

    public float bodyHp { get; private set; }
    public float leftHandHp { get; private set; }
    public float rightHandHp { get; private set; }

    private void Awake()
    {
        fireTrsOffset = new Vector3[9];

        float spacingX = 1;
        float spacingZ = 1;
        int index = 0;

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                float offsetX = (col - 1) * spacingX;
                float offsetZ = (1 - row) * spacingZ;
                fireTrsOffset[index] = new Vector3(offsetX, 0f, offsetZ);
                index++;
            }
        }
    }

    private void Start()
    {
        info = DataManager.instance.TableEnemy.Get(206);

        base.InitEnemyData(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, EEnemy.ROBOT6);

        bodyHp = info.MaxHp / 3; 
        leftHandHp = info.MaxHp / 3; 
        rightHandHp = info.MaxHp / 3; 
    }

    private void Update()
    {
        if(chargeLaserEffect.activeInHierarchy) 
        {
            UpdateChargeLaserLine();
        }
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        pattonNum = 1;

        switch (pattonNum)
        {
            case 0:
                AttackMissile();
                break;

            case 1:
                yield return StartCoroutine(StartAttackLaser());
                break;
        }


        yield return new WaitForSeconds(5f);

        isAttack = false;

        attackCoroutine = null;
    }

    private void AttackPatton()
    {
        int pattonNum = Random.Range(0, 1);
    }

    private void AttackMissile()
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                Vector3 offset = leftMissileTrs.right * x * spacing + leftMissileTrs.forward * z * spacing;
                Vector3 spawnPos = leftMissileTrs.position + offset;

                float randomSpeed = Random.Range(-20f, 10);
                float missileSpeed = speed + randomSpeed;

                GameObject missileObj = bulletPool.FindBullet
                    (EBullet.BOSSMISSILE, spawnPos, Quaternion.LookRotation(Vector3.up));

                BossMissile missile = missileObj.GetComponent<BossMissile>();

                missile.Init(myTarget, attackDmg, missileSpeed, EMissile.ENEMY);
            }
        }

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                Vector3 offset = rightMissileTrs.right * x * spacing + rightMissileTrs.forward * z * spacing;
                Vector3 spawnPos = rightMissileTrs.position + offset;

                float randomSpeed = Random.Range(-20f, 10);
                float missileSpeed = speed + randomSpeed;

                GameObject missileObj = bulletPool.FindBullet
                    (EBullet.BOSSMISSILE, spawnPos, Quaternion.LookRotation(Vector3.up));

                BossMissile missile = missileObj.GetComponent<BossMissile>();

                missile.Init(myTarget, attackDmg, missileSpeed, EMissile.ENEMY);
            }
        }
    }

    IEnumerator StartAttackLaser()
    {
        chargeLaserEffect.SetActive(true);

        Vector3 targetDir = (myTarget.position - laserFireTrs.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 1);

        yield return new WaitForSeconds(2);

        chargeLaserEffect.SetActive(false);
        laserEffect.SetActive(true);

        yield return new WaitForSeconds(3);

        laserEffect.SetActive(false);
    }

    private void UpdateChargeLaserLine()
    {
        Vector3 start = laserFireTrs.position;
        Vector3 dir = laserFireTrs.forward;

        Ray ray = new Ray(start, dir);
        Vector3 end = start + dir * 10;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            end = hit.point;
        }

        chargeLaserLine.SetPosition(0, start);
        chargeLaserLine.SetPosition(1, end);

    }

    public override void TakeDmg(float _dmg, bool _isHead)
    {
    }

    public void TakePartDmg(EBossPart _ePart, float _dmg)
    {
        switch (_ePart)
        {
            case EBossPart.BODY:
                if (bodyHp > 0)
                {
                    bodyHp -= _dmg;
                }
                break;
            case EBossPart.LEFT:
                if (leftHandHp > 0 && isLeftHand == true)
                {
                    leftHandHp -= _dmg;
                    DestroyPart(leftHandHp, leftHandBox, _ePart);
                }
                break;
            case EBossPart.RIGHT:
                if(rightHandHp > 0 && isRightHand == true) 
                {
                    rightHandHp -= _dmg;
                    DestroyPart(rightHandHp, rightHandBox, _ePart);
                }
                break;
        }

        curhp = bodyHp + leftHandHp + rightHandHp;

        TakeDmgEvent();

        if (curhp <= 0)
        {
            AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

            isDie = true;
        }
    }

    private void DestroyPart(float _hp, BoxCollider _box, EBossPart _part)
    {
        if (_hp <= 0)
        {
            AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

            if (_part == EBossPart.LEFT)
            {
                leftHandEffect.SetActive(true);

                isLeftHand = false;
            }
            else if( _part == EBossPart.RIGHT)
            {
                rightHandEffect.SetActive(true);

                isRightHand = false;
            }
        }
    }

    protected override void ChangeAnim(EEnemyAI _curState)
    {
        //_curState = aiState;

        //switch (_curState)
        //{
        //    case EEnemyAI.CREATE:
        //        anim.SetInteger("EnemyAnim", (int)EEnemyAnim.IDLE);
        //        break;
        //    case EEnemyAI.ATTACK:
        //        anim.SetInteger("EnemyAnim", (int)EEnemyAnim.ATTACK);
        //        break;
        //    case EEnemyAI.STAY:
        //        anim.SetInteger("EnemyAnim", (int)EEnemyAnim.IDLE);
        //        break;
        //    case EEnemyAI.DIE:
        //        anim.SetInteger("EnemyAnim", (int)EEnemyAnim.DIE);
        //        break;
        //}
    }
}
