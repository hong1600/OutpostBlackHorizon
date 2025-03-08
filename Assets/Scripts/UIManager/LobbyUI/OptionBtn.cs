using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionBtn : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] RectTransform[] rects;
    [SerializeField] Vector2[] onPos;
    [SerializeField] Vector2[] offPos;

    public void ClickOption()
    {
        UIManager.instance.OpenPanel(optionPanel, rects, onPos, offPos, true);
    }
}
