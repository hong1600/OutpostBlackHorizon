using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionPanel : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject optionPanel;
    [SerializeField] RectTransform[] rects;
    [SerializeField] Vector2[] onPos;
    [SerializeField] Vector2[] offPos;

    private void Start()
    {
        InputManager.instance.onInputEsc += OpenOptionPanel;
    }

    private void OpenOptionPanel()
    {
        if(!optionPanel.activeSelf)
        {
            cursor.SetActive(true);
            UIManager.instance.OpenPanel(optionPanel, rects, onPos, offPos, true);
        }
    }
}
