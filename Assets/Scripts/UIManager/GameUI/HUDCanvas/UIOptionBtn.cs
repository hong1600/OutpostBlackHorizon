using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionBtn : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;

    public void ClickOption()
    {
        optionPanel.SetActive(true);
    }
}
