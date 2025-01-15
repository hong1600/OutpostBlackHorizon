using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSkillBar : MonoBehaviour
{
    Unit unit;

    [SerializeField] Image skillBarFill;
    [SerializeField] Vector3 offset;
    [SerializeField] float skillValue;
    [SerializeField] Transform parent;

    private void Awake()
    {
        skillValue = 0f;
        skillBarFill.fillAmount = skillValue;
    }

    private void Update()
    {
        skillBar();
    }

    public void Init(Unit _unit)
    {
        unit = _unit;

        offset = new Vector3(0f, 3f, 0f);
    }

    public void skillBar()
    {
        skillValue = Mathf.Clamp(skillValue, 0, 5);

        if (skillValue < 5)
        {
            skillValue += Time.deltaTime;
        }
        else if(skillValue >= 5) 
        {
            unit.isSkill = true;

            if(unit.isSkill == false) 
            {
                skillValue = 0;
            }
        }

        skillBarFill.fillAmount = skillValue / 5;

        this.gameObject.transform.position = unit.transform.position + offset;

        Quaternion rotation = Quaternion.Euler(60f, 0f, 0f);
        this.gameObject.transform.rotation = rotation;
    }
}
