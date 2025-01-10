using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface ICustomMouse
{
    Vector2 GetMousePos();
}

public class CustomMouse : MonoBehaviour, ICustomMouse
{
    [SerializeField] Image customCursor;
    [SerializeField] float mouseSpeed;

    Vector2 mousePos;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mousePos = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        MouseCursorState();
        MouseCursorMove();
        SimulateMouseClick();
    }

    private void MouseCursorState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    private void MouseCursorMove()
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

    private void SimulateMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = mousePos
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }
    }

    public Vector2 GetMousePos() { return mousePos; }
}
