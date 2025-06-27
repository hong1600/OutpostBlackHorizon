using DG.Tweening;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSync : DirectProjectileSync
{
    public void Init(Transform _target, float _dmg, float _speed, EBulletType _eBulletType)
    {
        base.Init(_target, _dmg, _speed);

        type = _eBulletType;

        time = 3f;
    }

    [PunRPC]
    public void RPCInit(float _dmg, float _spd, int _eBulletType)
    {
        Init(null, _dmg, _spd, (EBulletType)_eBulletType);
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

    [PunRPC]
    protected override void RpcMove(Vector3 _pos, Quaternion _rot)
    {
        base.RpcMove(_pos, _rot);
    }

    protected override void ReturnPool()
    {
        bulletPool.ReturnBullet(EBullet.PLAYERBULLET, gameObject);
    }
}
