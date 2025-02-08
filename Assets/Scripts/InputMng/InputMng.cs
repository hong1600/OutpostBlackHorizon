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
    public static event Action<Vector3> onInputKey;

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

        iFieldSelector = fieldSelector;
        iCustomMouse = customMouse;
    }

    private void Update()
    {
        DownMouseLeft();
        DragMouseLeft();
        UpMouseLeft();
        DownMouseRight();
        InputMouse();
    }

    private void FixedUpdate()
    {
        InputKey();
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

    private void InputMouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        mouseDelta = new Vector2(mouseX, mouseY);

        if(mouseX != 0 || mouseY != 0) 
        {
            onInputMouse?.Invoke(mouseDelta);
        }
    }

    private void InputKey()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        keyDelta = new Vector3(moveX, 0, moveZ);

        onInputKey?.Invoke(keyDelta);
    }
}
