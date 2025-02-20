using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InputMng : MonoBehaviour
{
    public static event Action<Vector2> onLeftClickDown;
    public static event Action<Vector2> onLeftClickDrag;
    public static event Action<Vector2> onLeftClickUp;
    public static event Action onRightClickDown;
    public static event Action<Vector2> onInputMouse;
    public static event Action<Vector2> onInputKey;

    [SerializeField] FieldSelector fieldSelector;
    [SerializeField] CustomMouse customMouse;
    public IFieldSelector iFieldSelector;
    public ICustomMouse iCustomMouse;

    Vector2 startMousePos;
    Vector2 curMousePos;
    Vector2 endMousePos;

    public Vector2 mouseDelta { get; private set; }

    public Vector3 keyDelta { get; private set; }

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

        if (fieldSelector != null)
        {
            iFieldSelector = fieldSelector;
        }
        if (customMouse != null)
        {
            iCustomMouse = customMouse;
        }
    }

    private void Update()
    {
        DownMouseLeft();
        DragMouseLeft();
        UpMouseLeft();
        DownMouseRight();
        InputKey();
        InputMouse();
    }

    private void DownMouseLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = iCustomMouse.GetMousePos();
            onLeftClickDown?.Invoke(startMousePos);
        }
    }

    private void DragMouseLeft()
    {
        if (Input.GetMouseButton(0))
        {
            curMousePos = iCustomMouse.GetMousePos();
            onLeftClickDrag?.Invoke(iCustomMouse.GetMousePos());
        }
    }

    private void UpMouseLeft()
    {
        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = iCustomMouse.GetMousePos();
            onLeftClickUp?.Invoke(iCustomMouse.GetMousePos());
        }
    }

    private void DownMouseRight()
    {
        if (Input.GetMouseButtonDown(1))
        {
            onRightClickDown?.Invoke();
        }
    }

    private void InputKey()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector2 key = new Vector2(moveX, moveZ);

        if (key != Vector2.zero)
        {
            onInputKey?.Invoke(key);
        }
    }

    private void InputMouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        Vector2 mouse = new Vector2(mouseX, mouseY);

        if (mouse != Vector2.zero)
        {
            onInputMouse?.Invoke(mouse);
        }
    }
}
