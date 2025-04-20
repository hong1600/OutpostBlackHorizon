using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour, ITakeDmg
{
    TableField.Info info;
    EffectPool effectPool;

    [SerializeField] float maxHp;
    [SerializeField] float curHp;

    GameObject explosionEffect;

    bool isDestroy = false;

    private void Start()
    {
        info = DataManager.instance.TableField.GetFieldData(501);
        effectPool = ObjectPoolManager.instance.EffectPool;

        maxHp = info.Hp;
        curHp = info.Hp;
    }

    public void TakeDmg(float _dmg, bool _isHead)
    {
        curHp -= _dmg;

        if (curHp <= 0 && !isDestroy)
        {
            isDestroy = true;

            AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

            explosionEffect = effectPool.FindEffect(EEffect.ENEMYEXPLOSION, transform.position, Quaternion.identity);

            Invoke(nameof(ReturnEffect), 1);
        }
    }

    private void ReturnEffect()
    {
        effectPool.ReturnEffect(EEffect.ENEMYEXPLOSION, explosionEffect);

        Destroy(this.gameObject);
    }
}
