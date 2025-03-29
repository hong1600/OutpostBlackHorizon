using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    Enemy enemy;

    [SerializeField] GameObject hpBack;
    [SerializeField] Image hpValue;

    [SerializeField] Image smoothHpValue;
    [SerializeField] float smoothSpeed = 0.5f;

    SkinnedMeshRenderer skinRender;
    Vector3 hpBarPos;

    private void OnDisable()
    {
        enemy.onTakeDamage -= UpdateHpBar;
    }

    public void Init(Enemy _enemy, SkinnedMeshRenderer _skin)
    {
        enemy = _enemy;
        skinRender = _skin;

        enemy.onTakeDamage += UpdateHpBar;
        hpValue.fillAmount = 1;
        smoothHpValue.fillAmount = 1;
    }

    private void Update()
    {
        this.gameObject.transform.position = 
            skinRender.bounds.center + new Vector3(0, skinRender.bounds.extents.y + 0.5f, 0);

        this.gameObject.transform.LookAt(Camera.main.transform);
    }

    private void UpdateHpBar()
    {
        StopAllCoroutines();
        StartCoroutine(StartHpBar());
    }

    IEnumerator StartHpBar()
    {
        float curHp = enemy.curhp;
        float maxHp = enemy.maxHp;
        hpValue.fillAmount = curHp / maxHp;

        while (hpValue.fillAmount < smoothHpValue.fillAmount)
        {
            smoothHpValue.fillAmount = Mathf.Lerp
                (smoothHpValue.fillAmount, hpValue.fillAmount, smoothSpeed * Time.deltaTime);

            if (Mathf.Abs(smoothHpValue.fillAmount - hpValue.fillAmount) < 0.01f)
            {
                smoothHpValue.fillAmount = hpValue.fillAmount;
                yield break;
            }

            yield return null;
        }
    }
}
