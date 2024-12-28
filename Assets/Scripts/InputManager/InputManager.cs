using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector2> onLeftClick;

    public FieldSelector fieldSelector;
    public IFieldSelector iFieldSelector;

    private void Awake()
    {
        iFieldSelector = fieldSelector;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onLeftClick?.Invoke(Input.mousePosition);
        }
    }
}
