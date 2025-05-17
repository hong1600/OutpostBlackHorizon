using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : DirectProjectile
{
    public void Init(Transform _target, float _dmg, float _speed, EBulletType _eBulletType)
    {
        base.Init(_target, _dmg, _speed);

        type = _eBulletType;

        time = 3f;
    }

    protected override IEnumerator StartHitBullet(Vector3 _hitPos, Collider _hitObj)
    {
        if (_hitObj.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            hitAimImg.color = isHead ? Color.red : Color.white;
            hitAim.SetActive(true);
            hitAim.transform.DOScale(2f, 0.1f).OnKill(HideAim);
        }

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
