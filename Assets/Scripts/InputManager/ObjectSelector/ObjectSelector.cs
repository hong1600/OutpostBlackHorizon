using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectSelector
{
    GameObject getSelectObject();
}

public class ObjectSelector : MonoBehaviour, IObjectSelector
{
    public GameObject selectObject;

    private void OnEnable()
    {
        InputManager.onLeftClick -= objClick;
        InputManager.onLeftClick += objClick;
    }

    private void OnDisable()
    {
        InputManager.onLeftClick -= objClick;
    }

    public void objClick(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            selectObject = hit.collider.gameObject;
            Debug.Log($"{hit.collider.gameObject.name}");
        }
    }

    public GameObject getSelectObject() { return selectObject; }
}
