using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionBtn : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] RectTransform[] rects;
    [SerializeField] Vector2[] pos;

    public void ClickOption()
    {
        UIMng.instance.OpenPanel(optionPanel, rects, pos, true);
    }
}
