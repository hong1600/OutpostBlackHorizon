using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionBtn : MonoBehaviour
{
    public GameObject settingPanel;

    public void clickOption()
    {
        BtnManager.instance.openPanel(settingPanel);
    }
}
