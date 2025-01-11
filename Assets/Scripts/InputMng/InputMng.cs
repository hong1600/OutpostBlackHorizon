using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMng : MonoBehaviour
{
    public static event Action<Vector2> onLeftClickDown;
    public static event Action<Vector2> onLeftClickDrag;
    public static event Action<Vector2> onLeftClickUp;

    [SerializeField] FieldSelector fieldSelector;
    public IFieldSelector iFieldSelector;
    [SerializeField] CustomMouse customMouse;
    public ICustomMouse iCustomMouse;

    Vector2 startMousePos;
    Vector2 curMousePos;
    Vector2 endMousePos;

    private void Awake()
    {
        if (Shared.inputMng == null)
        {
            Shared.inputMng = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        iFieldSelector = fieldSelector;
        iCustomMouse = customMouse;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = iCustomMouse.GetMousePos();
            onLeftClickDown?.Invoke(startMousePos);
        }

        if (Input.GetMouseButton(0))
        {
            curMousePos = iCustomMouse.GetMousePos();
            onLeftClickDrag?.Invoke(iCustomMouse.GetMousePos());
        }

        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = iCustomMouse.GetMousePos();
            onLeftClickUp?.Invoke(iCustomMouse.GetMousePos());
        }
    }
}
