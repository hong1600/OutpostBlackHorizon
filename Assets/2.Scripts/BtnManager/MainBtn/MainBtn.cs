using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtn : BtnManager
{
    public GameObject heroPanel;
    public GameObject storePanel;
    public GameObject treasurePanel;
    public GameObject optionPanel;
    public GameObject settingPanel;
    public MainUI mainUI;

    bool option = false;

    public void StartBtn()
    {
        LoadScene.loadScene(2);
    }

    public void mainBtn()
    {
        heroPanel.SetActive(false);
        storePanel.SetActive(false);
        treasurePanel.SetActive(false);
    }

    public void heroBtn()
    {
        heroPanel.SetActive(true);
        storePanel.SetActive(false);
        treasurePanel.SetActive(false);
    }

    public void storeBtn()
    {
        heroPanel.SetActive(false);
        storePanel.SetActive(true);
        treasurePanel.SetActive(false);
    }

    public void treasureBtn()
    {
        heroPanel.SetActive(false);
        storePanel.SetActive(false);
        treasurePanel.SetActive(true);
    }

    public void treasureDcBtn(int index)
    {
        mainUI.treasureDc(index);
    }

    public void treasureUpgradeBtn()
    {
        mainUI.treasureUpgrade();
    }

    public void optionBtn() 
    {
        if (option == false)
        {
            optionPanel.SetActive(true);
            option = true;
        }
        else
        {
            optionPanel.SetActive(false);
            option = false;
        }
    }

    public void settingBtn()
    {
        settingPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void heroUpgradeBtn()
    {
        mainUI.HeroUpgrade();
    }

    public void storeUnitBtn(int index, string Name)
    {
        mainUI.storeSlotClick(index, Name);
    }

    public void storeResetBtn()
    {
        if (DataManager.instance.playerdata.gold >= 1000)
        {
            mainUI.storeSlotReset();
            DataManager.instance.playerdata.gold -= 1000;
        }
    }

    public void storeBuyBtn()
    {
        mainUI.storeBuy();
    }
}
