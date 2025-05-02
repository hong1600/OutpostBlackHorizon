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


    public bool isSkillCast;

    private void Awake()
    {
        skillValue = 0f;
        skillBarFill.fillAmount = skillValue;
        isSkillCast = false;
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

    private void skillBar()
    {
        if (unit.gameObject == null)
        {
            Destroy(this.gameObject);
        }

        if (unit == null) Destroy(this.gameObject);

        skillValue = Mathf.Clamp(skillValue, 0, 10);

        if (skillValue < 10)
        {
            skillValue += Time.deltaTime;
        }
        else if(skillValue >= 10 && !unit.isSkill) 
        {
            isSkillCast = true;
        }

        skillBarFill.fillAmount = skillValue / 10;
        this.gameObject.transform.position = unit.transform.position + offset;

        this.gameObject.transform.LookAt(Camera.main.transform);
    }

    public void ResetSkillBar()
    {
        isSkillCast = false;
        skillValue = 0f;
        skillBarFill.fillAmount = 0;
    }
}
