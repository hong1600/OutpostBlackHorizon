using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeBtn : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject mixPanel;
    [SerializeField] GameObject randomPanel;

    public void clickUpgradeBtn()
    {
        if (mixPanel.activeSelf || randomPanel.activeSelf)
        {
            mixPanel.SetActive(false);
            randomPanel.SetActive(false);
        }

        UIManager.instance.OpenPanel(upgradePanel, null, null, null, false);
    }
}
