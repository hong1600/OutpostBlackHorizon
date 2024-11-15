using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtn : BtnManager
{
    public TreasureUI treasureUI;
    public UnitUI unitUI;
    public StoreUI storeUI;

    public GameObject heroPanel;
    public GameObject storePanel;
    public GameObject treasurePanel;
    public GameObject optionPanel;
    public GameObject settingPanel;

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
        treasureUI.treasureDc(index);
    }

    public void treasureUpgradeBtn()
    {
        treasureUI.treasureUpgrade();
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
        unitUI.HeroUpgrade();
    }

    public void storeUnitBtn(int index, string Name)
    {
        storeUI.storeSlotClick(index, Name);
    }

    public void storeResetBtn()
    {
        if (DataManager.instance.playerdata.gold >= 1000)
        {
            storeUI.storeSlotReset();
            DataManager.instance.playerdata.gold -= 1000;
        }
    }

    public void storeBuyBtn()
    {
        storeUI.storeBuy();
    }
}
