using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropdownLayerFix : MonoBehaviour, IPointerClickHandler
{
    int forceOrder = 1;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(FixDropdownOrder());
    }

    IEnumerator FixDropdownOrder()
    {
        yield return null;

        var list = GameObject.Find("Dropdown List");
        if (list != null)
        {
            var canvas = list.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.overrideSorting = true;
                canvas.sortingOrder = forceOrder;
            }
        }

        var blocker = GameObject.Find("Blocker");
        if (blocker != null)
        {
            Canvas canvas = blocker.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.overrideSorting = true;
                canvas.sortingOrder = forceOrder - 1;
            }
        }
    }
}
