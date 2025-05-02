using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    UIManager uiManager;

    [SerializeField] GameObject optionPanel;
    [SerializeField] RectTransform[] rects;
    [SerializeField] Vector2[] onPos;
    [SerializeField] Vector2[] offPos;

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    public void OpenOption()
    {
        uiManager.OpenPanel(optionPanel, rects, onPos, offPos, true);

    }

}
