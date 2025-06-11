using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public event Action<Vector2> onLeftClickDown;
    public event Action<Vector2> onLeftClickDrag;
    public event Action<Vector2> onLeftClickUp;
    public event Action onRightClickDown;
    public event Action onInputMouse;
    public event Action<Vector2> onInputKey;
    public event Action onInputEsc;
    public event Action onInputB;
    public event Action onInputZ;
    public event Action onInputX;

    public FieldSelector fieldSelector;
    public CustomCursor cursor;

    Vector2 startMousePos;
    Vector2 curMousePos;
    Vector2 endMousePos;

    public Vector2 mouseDelta { get; private set; }

    public Vector3 keyDelta { get; private set; }

    public bool isInputLock = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (isInputLock) return;

        DownMouseLeft();
        DragMouseLeft();
        UpMouseLeft();
        DownMouseRight();
        InputKey();
        InputMouse();
        InputEsc();
        InputB();
        InputX();
        InputZ();
    }

    public void LockInput()
    {
        isInputLock = true;
    }

    public void UnlockInput()
    {
        isInputLock = false;
    }

    private void DownMouseLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = cursor.GetMousePos();
            onLeftClickDown?.Invoke(startMousePos);
        }
    }

    private void DragMouseLeft()
    {
        if (Input.GetMouseButton(0))
        {
            curMousePos = cursor.GetMousePos();
            onLeftClickDrag?.Invoke(cursor.GetMousePos());
        }
    }

    private void UpMouseLeft()
    {
        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = cursor.GetMousePos();
            onLeftClickUp?.Invoke(cursor.GetMousePos());
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
            onInputMouse?.Invoke();
        }
    }

    private void InputEsc()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            onInputEsc?.Invoke();
        }
    }

    private void InputB()
    {
        if(Input.GetKeyDown(KeyCode.B)) 
        {
            onInputB?.Invoke();
        }
    }

    private void InputX()
    {
        if(Input.GetKeyDown(KeyCode.X)) 
        {
            onInputX?.Invoke();
        }
    }

    private void InputZ()
    {
        if(Input.GetKeyDown(KeyCode.Z)) 
        {
            onInputZ?.Invoke();
        }
    }
}
