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

    public float bodyHp { get; private set; }
    public float leftHandHp { get; private set; }
    public float rightHandHp { get; private set; }

    private void Awake()
    {
        info = DataManager.instance.TableEnemy.Get(206);

        base.InitEnemyData(info.Name, info.MaxHp, info.Speed, info.AttackRange, info.AttackDmg, EEnemy.ROBOT6);

        bodyHp = info.MaxHp / 3; 
        leftHandHp = info.MaxHp / 3; 
        rightHandHp = info.MaxHp / 3; 
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        ITakeDmg iTakeDmg = myTarget.gameObject.GetComponent<ITakeDmg>();

        iTakeDmg.TakeDmg(attackDmg, false);

        yield return new WaitForSeconds(1f);

        isAttack = false;
        attackCoroutine = null;
    }

    public override void TakeDmg(float _dmg, bool _isHead)
    {
    }

    public void TakePartDmg(EBossPart _ePart, float _dmg)
    {
        curhp -= _dmg;

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

        TakeDmgEvent();

        if (curhp <= 0)
        {
            isDie = true;
        }
    }

    private void DestroyPart(float _hp, BoxCollider _box, EBossPart _part)
    {
        if (_hp <= 0)
        {
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
