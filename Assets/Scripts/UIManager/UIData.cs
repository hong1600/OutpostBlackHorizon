using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIData
{
    public RectTransform[] rects;
    public Vector2[] pos;

    public UIData(RectTransform[] _rects, Vector2[] _pos)
    {
        rects = _rects;
        pos = _pos;
    }
}
