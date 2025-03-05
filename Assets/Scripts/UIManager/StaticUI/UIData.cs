using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIData
{
    public RectTransform[] rects;
    public Vector2[] onPos;
    public Vector2[] offPos;

    public UIData(RectTransform[] _rects, Vector2[] _onPos, Vector2[] _offPos)
    {
        rects = _rects;
        onPos = _onPos;
        offPos = _offPos;
    }
}
