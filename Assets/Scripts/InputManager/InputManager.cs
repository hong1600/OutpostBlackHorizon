using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public event Action<Vector2> onLeftClickDown;
    public event Action<Vector2> onLeftClickDrag;
    public event Action<Vector2> onLeftClickUp;
    public event Action onRightClickDown;
    public event Action<Vector2> onInputMouse;
    public event Action<Vector2> onInputKey;
    public event Action onInputEsc;

    [SerializeField] FieldSelector fieldSelector;
    [SerializeField] CustomMouse customMouse;
    public FieldSelector FieldSelector { get; private set; }
    public CustomMouse CustomMouse { get; private set; }

    Vector2 startMousePos;
    Vector2 curMousePos;
    Vector2 endMousePos;

    public Vector2 mouseDelta { get; private set; }

    public Vector3 keyDelta { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }

        if (fieldSelector != null)
        {
            FieldSelector = fieldSelector;
        }
        if (customMouse != null)
        {
            CustomMouse = customMouse;
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
        InputEsc();
    }

    private void DownMouseLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = customMouse.GetMousePos();
            onLeftClickDown?.Invoke(startMousePos);
        }
    }

    private void DragMouseLeft()
    {
        if (Input.GetMouseButton(0))
        {
            curMousePos = customMouse.GetMousePos();
            onLeftClickDrag?.Invoke(customMouse.GetMousePos());
        }
    }

    private void UpMouseLeft()
    {
        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = customMouse.GetMousePos();
            onLeftClickUp?.Invoke(customMouse.GetMousePos());
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

    private void InputEsc()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            onInputEsc?.Invoke();
        }
    }
}
