using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionBtn : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;

    public void ClickOption()
    {
        UIMng.instance.OpenPanel(settingPanel);
    }
}
