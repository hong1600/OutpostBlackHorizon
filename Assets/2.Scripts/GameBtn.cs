using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBtn : MonoBehaviour
{

    [SerializeField] GameObject randomPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject mixPanel;

    [SerializeField] TextMeshProUGUI speedText;

    bool speed1 = true;

    public void spawnBtn()
    {
        
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
        if(speed1 == true)
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

}
