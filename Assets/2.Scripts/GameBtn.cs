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

    public void spawnBtn()
    {
        if (GameManager.Instance.Gold > GameManager.Instance.SpawnGold)
        {
            GameManager.Instance.spawnUnit();
        }
    }

    public void randomBtn()
    {
        randomPanel.SetActive(true);
    }

    public void upgradeBtn()
    {
        upgradePanel.SetActive(true);
    }

    public void mixBtn()
    {
        mixPanel.SetActive(true);
    }

    public void randomCloseBtn()
    {
        randomPanel.SetActive(false);
    }

    public void upgradeCloseBtn()
    {
        upgradePanel.SetActive(false);
    }

    public void mixCloseBtn()
    {
        mixPanel.SetActive(false);
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
        settingPanel.SetActive(true);
    }

    public void settingClose()
    {
        settingPanel.SetActive(false);
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
