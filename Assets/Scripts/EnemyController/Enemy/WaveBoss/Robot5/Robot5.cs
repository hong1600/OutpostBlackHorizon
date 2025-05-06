using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Robot5 : WaveBoss
{
    [SerializeField] float totalTime;
    [SerializeField] float time;
    [SerializeField] float interval;

    public override void Init(string _name, float _maxHp, float _spd, float _range, float _dmg, EEnemy _eEnemy)
    {
        base.Init(_name, _maxHp, _spd, _range, _dmg, _eEnemy);
    }

    protected override IEnumerator StartAttack()
    {
        isAttack = true;

        while (time < totalTime)
        {
            render.material = hitMat;
            yield return new WaitForSeconds(interval);
            render.material = originMat;
            yield return new WaitForSeconds(interval);

            time += interval * 2;

            interval = Mathf.Max(0.05f, interval * 0.6f);
        }

        Explode();
    }

    private void Explode()
    {
        GameObject effect = effectPool.FindEffect(EEffect.ROCKETEXPLOSION, transform.position, Quaternion.identity);
        Explosion explosion = effect.GetComponent<Explosion>();
        explosion.Init(attackDmg, EMissile.ENEMY);

        if (enemyHpBar != null)
        {
            hpBarPool.ReturnHpBar(EHpBar.NORMAL, enemyHpBar.gameObject);
        }

        enemyPool.ReturnEnemy(eEnemy, this.gameObject);
    }
}
