using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    [SerializeField] GameObject hpBack;
    [SerializeField] Image hpValue;

    [SerializeField] Image smoothHpValue;
    [SerializeField] float smoothSpeed = 0.5f;

    [SerializeField] Vector3 offset = new Vector3(0f, 3f, 0f);

    private void OnDisable()
    {
        enemy.onTakeDamage -= UpdateHpBar;
    }

    public void Init(Enemy _enemy)
    {
        enemy = _enemy;

        enemy.onTakeDamage += UpdateHpBar;
        hpValue.fillAmount = 1;
        smoothHpValue.fillAmount = 1;
    }

    private void Update()
    {
        this.gameObject.transform.position = enemy.transform.position + offset;

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
        float maxHp = enemy.enemyHp;
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
