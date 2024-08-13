using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBtn : MonoBehaviour
{
    [SerializeField] GameObject heroPanel;
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject treasurePanel;

    public void StartBtn()
    {
        SceneManager.LoadScene(1);
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
    public void mainBtn()
    {
        heroPanel.SetActive(false);
        storePanel.SetActive(false);
        treasurePanel.SetActive(false);
    }
    public void treasureBtn()
    {
        heroPanel.SetActive(false);
        storePanel.SetActive(false);
        treasurePanel.SetActive(true);
    }
}
