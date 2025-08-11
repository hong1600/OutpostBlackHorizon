using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    GameObject lastEventObj = null;
    PointerEventData eventData;

    [SerializeField] Image cursorImg;
    [SerializeField] float mouseSpeed;

    Vector2 mousePos;

    private void Awake()
    {
        InputManager.instance.cursor = this;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        InputManager.instance.onInputMouse += CheckUIOrObj;
        InputManager.instance.onLeftClickUp += ClickMouseEvent;

        mousePos = new Vector2(Screen.width / 2, Screen.height / 2);
        cursorImg.rectTransform.position = mousePos;
    }

    private void Update()
    {
        UpdateCursorLock();
        MoveMouseCursor();
    }

    private void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }
        }
    }

    private void MoveMouseCursor()
    {
        mousePos = Input.mousePosition;
        
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cursorImg.canvas.transform as RectTransform,
            mousePos,
            cursorImg.canvas.worldCamera,
            out Vector2 localPoint
        );
        
        cursorImg.rectTransform.localPosition = localPoint;
    }

    private void CheckUIOrObj()
    {
        GameObject target = CheckUI();

        if (target == null) 
        {
            target = CheckObj();
        }

        if (target != lastEventObj)
        {
            if (lastEventObj != null)
            {
                ExecuteEvents.Execute(lastEventObj, eventData, ExecuteEvents.pointerExitHandler);
            }
            if (target != null)
            {
                ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerEnterHandler);
            }

            lastEventObj = target;
        }
    }

    private GameObject CheckUI()
    {
        if (EventSystem.current == null) return null;

        eventData = new PointerEventData(EventSystem.current)
        {
            position = mousePos
        };

        List<RaycastResult> resultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, resultList);

        GameObject closeUI = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < resultList.Count; i++) 
        {
            RaycastResult result = resultList[i];

            if (result.gameObject.name.Contains("Caret")) continue;

            if (result.distance < minDistance)
            {
                minDistance = result.distance;
                closeUI = result.gameObject;
            }
        }

        return closeUI;
    }

    private GameObject CheckObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private void ClickMouseEvent(Vector2 _pos)
    {
        CheckUIOrObj();

        if (lastEventObj != null)
        {
            //ExecuteEvents.Execute(lastEventObj, eventData, ExecuteEvents.pointerClickHandler);
            //Debug.Log(lastEventObj);

            if (lastEventObj.TryGetComponent<Button>(out Button button))
            {
                if (button.interactable) 
                {
                    AudioManager.instance.PlaySfxUI(ESfx.CLICK);
                }
            }
            else if (ExecuteEvents.GetEventHandler<IPointerClickHandler>(lastEventObj) != null)
            {
                AudioManager.instance.PlaySfxUI(ESfx.CLICK);
            }
        }
    }

    public Vector2 GetMousePos() { return mousePos; }
}
