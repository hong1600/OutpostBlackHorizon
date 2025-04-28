using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour, ITakeDmg
{
    TableField.Info info;
    EffectPool effectPool;

    [SerializeField] float maxHp;
    [SerializeField] float curHp;

    [SerializeField] Renderer rend;
    [SerializeField] Material hitMat;
    Material originMat;

    GameObject explosionEffect;

    bool isDestroy = false;

    private void Awake()
    {
        originMat = rend.material;
    }

    private void Start()
    {
        info = DataManager.instance.TableField.GetFieldData(501);
        effectPool = ObjectPoolManager.instance.EffectPool;

        if (info != null)
        {
            maxHp = info.Hp;
            curHp = info.Hp;
        }
    }

    public void TakeDmg(float _dmg, bool _isHead)
    {
        curHp -= _dmg;

        rend.material = hitMat;

        Invoke(nameof(UpdateMat), 0.2f);

        if (curHp <= 0 && !isDestroy)
        {
            isDestroy = true;

            AudioManager.instance.PlaySfx(ESfx.EXPLOSION, transform.position);

            explosionEffect = effectPool.FindEffect(EEffect.ENEMYEXPLOSION, transform.position, Quaternion.identity);

            Invoke(nameof(ReturnEffect), 1);
        }
    }

    private void UpdateMat()
    {
        rend.material = originMat;
    }

    private void ReturnEffect()
    {
        effectPool.ReturnEffect(EEffect.ENEMYEXPLOSION, explosionEffect);

        Destroy(this.gameObject);
    }
}
