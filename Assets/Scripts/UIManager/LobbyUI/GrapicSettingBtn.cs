using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapicSettingBtn : MonoBehaviour
{
    [SerializeField] GameObject grapicPanel;
    [SerializeField] RectTransform[] rects;
    [SerializeField] Vector2[] onPos;
    [SerializeField] Vector2[] offPos;

    public void ClickGrapic()
    {
        UIMng.instance.OpenPanel(grapicPanel, rects, onPos, offPos, true);
    }
}
