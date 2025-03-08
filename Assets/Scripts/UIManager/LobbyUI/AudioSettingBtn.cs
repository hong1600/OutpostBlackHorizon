using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingBtn : MonoBehaviour
{
    [SerializeField] GameObject audioPanel;
    [SerializeField] RectTransform[] rects;
    [SerializeField] Vector2[] onPos;
    [SerializeField] Vector2[] offPos;

    public void ClickAudio()
    {
        UIManager.instance.OpenPanel(audioPanel, rects, onPos, offPos, true);
    }

}
