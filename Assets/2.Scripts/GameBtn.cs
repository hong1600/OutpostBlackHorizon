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

    public void showPanelOpen(GameObject targetPanel)
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

    public void clickPerClostBtn(GameObject perPanel)
    {
        if(perPanel.activeSelf) 
        {
            closeBtn(perPanel);
        }
    }

    public void clickUnitDcClostBtn(GameObject unitDcPanel)
    {
        if (unitDcPanel.activeSelf)
        {
            closeBtn(unitDcPanel);
        }
    }

    public void spawnBtn()
    {
        if (GameManager.Instance.myGold > GameManager.Instance.unitMng.spawnGold)
        {
            GameManager.Instance.unitMng.spawnUnit();
        }
    }

    public void randomBtn()
    {
        showPanelOpen(randomPanel);
    }

    public void randomSpwanBtn(int index)
    {
        GameManager.Instance.unitMng.randSpawn(index);
    }

    public void upgradeBtn()
    {
        showPanelOpen(upgradePanel);
    }


    public void unitUpgradeBtn(int index)
    {
        GameManager.Instance.upgradeMng.unitUpgradeBtn(index);
    }

    public void upgradeSpawnPer()
    {
        showPanelOpen(spawnperPaenl);
    }

    public void mixBtn()
    {
        showPanelOpen(mixPanel);
        GameManager.Instance.unitMng.canMixUnit();
    }

    public void mixUnitSpawnBtn()
    {
        GameManager.Instance.unitMng.MixUnitSpawn();
    }

    public void spawnWaveBossBtn()
    {
        GameManager.Instance.gameFlow.spawnWaveBoss();
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
