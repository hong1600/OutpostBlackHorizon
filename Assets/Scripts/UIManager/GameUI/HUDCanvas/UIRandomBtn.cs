using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRandomBtn : MonoBehaviour
{
    [SerializeField] GameObject randomPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject mixPanel;


    public void ClickRandomBtn()
    {
        if (mixPanel.activeSelf || upgradePanel.activeSelf)
        {
            mixPanel.SetActive(false);
            upgradePanel.SetActive(false);
        }

        UIMng.instance.OpenPanel(randomPanel);
    }
}
