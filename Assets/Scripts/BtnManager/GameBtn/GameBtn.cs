using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBtn : BtnManager
{
    [SerializeField] GameObject randomPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject mixPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject spawnperPaenl;
    [SerializeField] TextMeshProUGUI speedText;
    bool speed1;

    private void Start()
    {
        speed1 = true;
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
        if (GameManager.Instance.myGold > GameManager.Instance.unitMng.unitSpawner.spawnGold)
        {
            GameManager.Instance.unitMng.unitSpawner.spawnUnit();
        }
    }

    public void randomBtn()
    {
        showPanelOpen(randomPanel);
    }

    public void randomSpwanBtn(int index)
    {
        GameManager.Instance.unitMng.unitRandomSpawner.randSpawn(index);
    }

    public void upgradeBtn()
    {
        showPanelOpen(upgradePanel);
    }


    public void unitUpgradeBtn(int index)
    {
        GameManager.Instance.unitMng.unitUpgrader.unitUpgrade(index);
    }

    public void upgradeSpawnPer()
    {
        showPanelOpen(spawnperPaenl);
    }

    public void mixBtn()
    {
        showPanelOpen(mixPanel);
        GameManager.Instance.unitMng.unitMixer.unitCanMix();
    }

    public void mixUnitSpawnBtn()
    {
        GameManager.Instance.unitMng.unitMixer.unitMixSpawn();
    }

    public void spawnWaveBossBtn()
    {
        GameManager.Instance.enemyMng.waveBossSpawner.spawnWaveBoss();
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
}
