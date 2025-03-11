using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomMouse : MonoBehaviour
{
    GameObject lastEventObj = null;
    PointerEventData eventData;

    [SerializeField] Image customCursor;
    [SerializeField] float mouseSpeed;

    Vector2 mousePos;

    private void Awake()
    {
        InputManager.instance.customMouse = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputManager.instance.onInputMouse += UpdateMouseEvent;
        InputManager.instance.onLeftClickUp += OnClickMouseEvent;

        mousePos = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void OnEnable()
    {
        if (InputManager.instance != null)
        {
            InputManager.instance.onInputMouse += UpdateMouseEvent;
            InputManager.instance.onLeftClickUp += OnClickMouseEvent;
        }
    }

    private void OnDisable()
    {
        InputManager.instance.onInputMouse -= UpdateMouseEvent;
        InputManager.instance.onLeftClickUp -= OnClickMouseEvent;
    }

    private void Update()
    {
        UpdateMouseCursorState();
        MoveMouseCursor();
    }

    private void UpdateMouseCursorState()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void MoveMouseCursor()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed;

        mousePos += new Vector2(mouseX, mouseY);

        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            customCursor.canvas.transform as RectTransform,
            mousePos,
            customCursor.canvas.worldCamera,
            out Vector2 localPoint
        );

        customCursor.rectTransform.localPosition = localPoint;
    }

    private void UpdateMouseEvent(Vector2 _pos)
    {
        eventData = new PointerEventData(EventSystem.current)
        {
            position = mousePos
        };

        List<RaycastResult> resultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, resultList);

        GameObject eventObj = null;
        float minDepth = float.MaxValue;

        for (int i = 0; i < resultList.Count; i++)
        {
            if (resultList[i].depth < minDepth)
            {
                minDepth = resultList[i].depth;
                eventObj = resultList[i].gameObject;
            }
        }

        if (eventObj != lastEventObj)
        {
            if (lastEventObj != null)
            {
                ExecuteEvents.Execute(lastEventObj, eventData, ExecuteEvents.pointerExitHandler);
            }

            if (eventObj != null)
            {
                ExecuteEvents.Execute(eventObj, eventData, ExecuteEvents.pointerEnterHandler);
            }

            lastEventObj = eventObj;
        }
    }

    private void OnClickMouseEvent(Vector2 _pos)
    {
        if (lastEventObj != null)
        {
            ExecuteEvents.Execute(lastEventObj, eventData, ExecuteEvents.pointerClickHandler);
        }
    }

    public Vector2 GetMousePos() { return mousePos; }
}
