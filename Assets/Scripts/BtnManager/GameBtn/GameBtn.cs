using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBtn : BtnManager
{
    public IGoldCoin iGoldCoin;
    public IWaveBoss iWaveBoss;

    public UnitSpawner unitSpawner;
    public UnitUpgrader unitUpgrader;
    public UnitMixer unitMixer;
    public UnitRandomSpawner unitRandomSpawner;

    public GameObject randomPanel;
    public GameObject upgradePanel;
    public GameObject mixPanel;
    public GameObject settingPanel;
    public GameObject spawnperPaenl;
    public TextMeshProUGUI speedText;
    public bool speed1;

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
        if (iGoldCoin.getGold() > unitSpawner.spawnGold)
        {
            unitSpawner.spawnUnit();
        }
    }

    public void randomBtn()
    {
        showPanelOpen(randomPanel);
    }

    public void randomSpwanBtn(int index)
    {
        unitRandomSpawner.randSpawn(index);
    }

    public void upgradeBtn()
    {
        showPanelOpen(upgradePanel);
    }


    public void unitUpgradeBtn(int index)
    {
        unitUpgrader.unitUpgrade(index);
    }

    public void upgradeSpawnPer()
    {
        showPanelOpen(spawnperPaenl);
    }

    public void mixBtn()
    {
        showPanelOpen(mixPanel);
        unitMixer.unitCanMix();
    }

    public void mixUnitSpawnBtn()
    {
        unitMixer.unitMixSpawn();
    }

    public void spawnWaveBossBtn()
    {
        iWaveBoss.spawnWaveBoss();
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
