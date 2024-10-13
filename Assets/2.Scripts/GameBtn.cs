using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBtn : MonoBehaviour
{
    [SerializeField] GameObject randomPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject mixPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject spawnperPaenl;
    [SerializeField] Image bgmImg;
    [SerializeField] Image sfxImg;
    [SerializeField] TextMeshProUGUI speedText;
    bool speed1;
    bool bgm;
    bool sfx;

    private void Start()
    {
        speed1 = true;
        bgm = false;
        sfx = false;
    }

    private void showPanelOpen(GameObject targetPanel)
    {
        if (targetPanel.activeSelf == false)
        {
            targetPanel.SetActive(true);
            targetPanel.transform.localScale = Vector3.zero;
            targetPanel.transform.DOScale(Vector3.one, 0.2f);
        }
        else 
        {
            return;
        }
    }

    public void closeBtn(GameObject button)
    {
        Transform buttonParent = button.transform.parent;

        if (buttonParent != null)
        {
            buttonParent.gameObject.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                buttonParent.gameObject.SetActive(false);
            });
        }
    }

    public void spawnBtn()
    {
        if (GameManager.Instance.Gold > GameManager.Instance.SpawnGold)
        {
            GameManager.Instance.spawnUnit();
        }
    }

    public void unitUpgradeBtn(int index)
    {
        List<Unit> unitList = GameManager.Instance.curUnitList;

        if (index == 0)
        {
            if (GameManager.Instance.UpgradeLevel1 < 6)
            {
                GameManager.Instance.Gold -= GameManager.Instance.UpgradeCost1;
                GameManager.Instance.UpgradeCost1 += 10f;
                GameManager.Instance.UpgradeLevel1 += 1f;

                for (int i = 0; i < unitList.Count; i++)
                {
                    if (unitList[i].unitGrade == 0)
                    {
                        unitList[i].upgrade();
                    }
                }
            }
        }
        if (index == 1)
        {
            if (GameManager.Instance.UpgradeLevel2 < 6)
            {
                GameManager.Instance.Gold -= GameManager.Instance.UpgradeCost2;
                GameManager.Instance.UpgradeCost2 += 10f;
                GameManager.Instance.UpgradeLevel2 += 1f;

                for (int i = 0; i < unitList.Count; i++)
                {
                    if (unitList[i].unitGrade == 1)
                    {
                        unitList[i].upgrade();
                    }
                }

            }
        }
        if (index == 2)
        {
            if (GameManager.Instance.UpgradeLevel3 < 6)
            {
                GameManager.Instance.Gold -= GameManager.Instance.UpgradeCost3;
                GameManager.Instance.UpgradeCost3 += 10f;
                GameManager.Instance.UpgradeLevel3 += 1f;

                for (int i = 0; i < unitList.Count; i++)
                {
                    {
                        unitList[i].upgrade();
                    }
                }

            }
        }
        if (index == 3)
        {
            if (GameManager.Instance.UpgradeLevel4 < 6)
            {
                GameManager.Instance.Gold -= GameManager.Instance.UpgradeCost4;
                GameManager.Instance.UpgradeCost4 += 10f;
                GameManager.Instance.UpgradeLevel4 += 1f;
            }
        }
    }

    public void randomBtn()
    {
        showPanelOpen(randomPanel);
    }

    public void upgradeBtn()
    {
        showPanelOpen(upgradePanel);
    }

    public void spawnPer()
    {
        showPanelOpen(spawnperPaenl);
    }

    public void mixBtn()
    {
        showPanelOpen(mixPanel);
    }

    public void speedUpBtn()
    {
        if (speed1 == true)
        {
            Time.timeScale = 2f;
            speed1 = false;
            speedText.text = "X2";
        }
        else
        {
            Time.timeScale = 1f;
            speed1 = true;
            speedText.text = "X1";
        }
    }

    public void setting()
    {
        showPanelOpen(settingPanel);
    }

    public void bgmBtn()
    {
        if (bgm == false)
        {
            bgmImg.color = new Color(1, 0, 0, 1);
            bgm = true;
        }
        else
        {
            bgmImg.color = new Color(1, 0, 0, 0);
            bgm = false;
        }
    }

    public void sfxBtn()
    {
        if (sfx == false)
        {
            sfxImg.color = new Color(1, 0, 0, 1);
            sfx = true;
        }
        else
        {
            sfxImg.color = new Color(1, 0, 0, 0);
            sfx = false;
        }
    }
}
