using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot6 : Boss
{
    [SerializeField] BoxCollider leftHandBox;
    [SerializeField] BoxCollider rightHandBox;

    [SerializeField] GameObject leftHandEffect;
    [SerializeField] GameObject rightHandEffect;

    bool isLeftHand = true;
    bool isRightHand = true;

    int pattonNum;

    [Header("Missile")]
    [SerializeField] float speed = 50;
    [SerializeField] float spacing = 10f;
    [SerializeField] Transform leftMissileTrs;
    [SerializeField] Transform rightMissileTrs;
    Vector3[] fireTrsOffset;

    [Header("Laser")]
    [SerializeField] Transform laserFireTrs;
    [SerializeField] GameObject laserEffect;
    [SerializeField] LineRenderer laserLine;
    [SerializeField] GameObject chargeLaserEffect;
    [SerializeField] LineRenderer chargeLaserLine;
    Material laserMat;
    Material chargeMat;
    Vector3 smoothLaserEnd;
    float emissionPower = 0f;
    float maxEmission = 2f;
    float laserTimer = 0.5f;

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

        chargeMat = chargeLaserLine.material;
        laserMat = laserLine.material;
    }

    public override void Init(string _name, float _maxHp, float _spd, float _range, float _dmg, EEnemy _eEnemy)
    {
        base.Start();

        base.Init(_name, _maxHp, _spd, _range, _dmg, _eEnemy);

        bodyHp = maxHp / 3;
        leftHandHp = maxHp / 3;
        rightHandHp = maxHp / 3;
    }

    protected override void Update()
    {
        base.Update();

        UpdateLaser();
        UpdateChargeLaser();
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        ChangePatton();

        switch (pattonNum)
        {
            case 0:
                StartCoroutine(StartAttackMissile(leftMissileTrs));
                StartCoroutine(StartAttackMissile(rightMissileTrs));
                break;

            case 1:
                yield return StartCoroutine(StartAttackLaser());
                break;
        }


        yield return new WaitForSeconds(5f);

        base.CheckTarget();

        isAttack = false;

        attackCoroutine = null;
    }

    private void ChangePatton()
    {
        pattonNum = Random.Range(0, 2);
    }

    IEnumerator StartAttackMissile(Transform _fireTrs)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                Vector3 offset = _fireTrs.right * x * spacing + _fireTrs.forward * z * spacing;
                Vector3 spawnPos = _fireTrs.position + offset;

                float randomSpeed = Random.Range(-20f, 10);
                float missileSpeed = speed + randomSpeed;

                GameObject missileObj = bulletPool.FindBullet
                    (EBullet.BOSSMISSILE, spawnPos, Quaternion.LookRotation(Vector3.up));

                BossMissile missile = missileObj.GetComponent<BossMissile>();

                missile.Init(myTarget, attackDmg, missileSpeed, EMissile.ENEMY);

                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    IEnumerator StartAttackLaser()
    {
        chargeLaserEffect.SetActive(true);

        Vector3 targetDir = (myTarget.position - laserFireTrs.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 1);

        yield return new WaitForSeconds(3);

        chargeLaserEffect.SetActive(false);
        laserEffect.SetActive(true);

        yield return new WaitForSeconds(4);

        laserEffect.SetActive(false);
    }

    private void UpdateLaser()
    {
        if(laserEffect.activeSelf) 
        {
            if (myTarget == null)
            {
                base.CheckTarget();
            }

            if (myTarget == null) return;


            Vector3 start = laserFireTrs.position;
            Vector3 dir = (myTarget.position - laserFireTrs.position).normalized;
            Vector3 end = start + dir * 2000;

            if (smoothLaserEnd == Vector3.zero)
                smoothLaserEnd = end;

            smoothLaserEnd = Vector3.Lerp(smoothLaserEnd, end, Time.deltaTime * 2.5f);

            laserLine.SetPosition(0, start);
            laserLine.SetPosition(1, smoothLaserEnd);

            Ray ray = new Ray(start, smoothLaserEnd);

            RaycastHit[] hits = Physics.SphereCastAll(ray, 2f, 2000f, LayerMask.GetMask("Player", "Field"));

            laserTimer -= Time.deltaTime;

            if (laserTimer <= 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.GetComponent<ITakeDmg>() != null)
                    {
                        ITakeDmg iTakeDmg = hits[i].collider.gameObject.GetComponent<ITakeDmg>();

                        iTakeDmg.TakeDmg(attackDmg, false);
                    }
                }

                laserTimer = 0.5f;
            }
        }
    }

    private void UpdateChargeLaser()
    {
        if (chargeLaserEffect.activeSelf)
        {
            if (myTarget == null)
            {
                base.CheckTarget();
            }

            if (myTarget == null) return;

            emissionPower = Mathf.Min(maxEmission, emissionPower + Time.deltaTime);

            Vector3 start = laserFireTrs.position;
            Vector3 dir = (myTarget.position - laserFireTrs.position).normalized;
            Vector3 end = start + dir * 2000;

            if (smoothLaserEnd == Vector3.zero)
                smoothLaserEnd = end;

            smoothLaserEnd = Vector3.Lerp(smoothLaserEnd, end, Time.deltaTime * 4f);

            chargeLaserLine.SetPosition(0, start);
            chargeLaserLine.SetPosition(1, smoothLaserEnd);
        }
        else
        {
            emissionPower = 0f;
        }

        chargeMat.SetColor("_EmissionColor", Color.red * emissionPower);
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
}
