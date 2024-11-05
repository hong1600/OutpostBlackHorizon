using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUpgrader : MonoBehaviour
{
    public UnitMng unitMng;

    public int upgradeCost1;
    public int upgradeCost2;
    public int upgradeCost3;
    public int upgradeCost4;
    public int upgradeLevel1;
    public int upgradeLevel2;
    public int upgradeLevel3;
    public int upgradeLevel4;

    public void initialize(UnitMng manager)
    {
        unitMng = manager;
    }

    public void upgradeBtn(int index)
    {
        if (upgradeCost1 < GameManager.Instance.myGold && index == 1)
        {
            GameManager.Instance.myGold -= upgradeCost1;
            upgradeCost1 *= 2;
            upgradeLevel1++;
        }
    }

    public void unitUpgradeBtn(int index)
    {
        int grade = index;

        List<Unit> unitList = GameManager.Instance.unitMng.curUnitList;

        if (grade == 0)
        {
            if (upgradeLevel1 < 6)
            {
                GameManager.Instance.myGold -= upgradeCost1;
                upgradeCost1 += 30;
                upgradeLevel1 += 1;

                for (int i = 0; i < unitList.Count; i++)
                {
                    if (unitList[i].unitGrade == 0)
                    {
                        unitList[i].upgrade();
                    }
                }
            }
        }
        if (grade == 1)
        {
            if (upgradeLevel2 < 6)
            {
                GameManager.Instance.myGold -= upgradeCost2;
                upgradeCost2 += 50;
                upgradeLevel2 += 1;

                for (int i = 0; i < unitList.Count; i++)
                {
                    if (unitList[i].unitGrade == 1)
                    {
                        unitList[i].upgrade();
                    }
                }

            }
        }
        if (grade == 2)
        {
            if (upgradeLevel3 < 6)
            {
                GameManager.Instance.myCoin -= upgradeCost3;
                upgradeCost3 += 1;
                upgradeLevel3 += 1;

                for (int i = 0; i < unitList.Count; i++)
                {
                    {
                        unitList[i].upgrade();
                    }
                }

            }
        }
        if (grade == 3)
        {
            if (upgradeLevel4 < 6)
            {
                GameManager.Instance.myGold -= upgradeCost4;
                upgradeCost4 += 100;
                upgradeLevel4 += 1;
            }
        }
    }

}
