using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public override void Init(Transform _target, float _dmg, float _speed)
    {
        base.Init(_target, _dmg, _speed);

        time = 3f;
    }

    protected override IEnumerator StartHitBullet(Vector3 _hitPos, Collider _hitObj)
    {
        hitAimImg.color = isHead ? Color.red : Color.white;
        hitAim.SetActive(true);
        hitAim.transform.DOScale(2f, 0.1f).OnKill(HideAim);

        return base.StartHitBullet(_hitPos, _hitObj);
    }

    private void HideAim()
    {
        hitAim.transform.localScale = new Vector3(1, 1, 1);
        hitAim.SetActive(false);
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.PLAYERBULLET, gameObject);
    }
}
