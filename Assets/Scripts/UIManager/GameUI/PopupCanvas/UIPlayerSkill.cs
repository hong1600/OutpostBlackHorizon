using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSkill : MonoBehaviour
{
    AirStrike airStrike;
    GuideMissile guideMissile;

    [SerializeField] Image skill1Value;
    [SerializeField] Image skill2Value;

    [SerializeField] TextMeshProUGUI skill1Text;
    [SerializeField] TextMeshProUGUI skill2Text;

    private void Start()
    {
        airStrike = PlayerManager.instance.airStrike;
        guideMissile = PlayerManager.instance.guideMissile;
    }

    private void Update()
    {
        UpdateSkill1();
        UpdateSkill2();
    }

    private void UpdateSkill1()
    {
        if (airStrike != null)
        {
            float cool = Mathf.Clamp01(airStrike.coolTime / airStrike.maxCollTime);

            skill1Value.fillAmount = Mathf.Lerp(skill1Value.fillAmount, cool, Time.deltaTime * 10f);

            if (airStrike.coolTime > 0)
            {
                skill1Text.text = airStrike.coolTime.ToString("F1");
            }
            else
            {
                skill1Text.text = "";
            }
        }
    }
    private void UpdateSkill2()
    {
        if (airStrike != null)
        {
            float cool = Mathf.Clamp01(guideMissile.coolTime / guideMissile.maxCoolTime);

            skill2Value.fillAmount = Mathf.Lerp(skill2Value.fillAmount, cool, Time.deltaTime * 10f);

            if (guideMissile.coolTime > 0)
            {
                skill2Text.text = guideMissile.coolTime.ToString("F1");
            }
            else
            {
                skill2Text.text = "";
            }
        }
    }
}
