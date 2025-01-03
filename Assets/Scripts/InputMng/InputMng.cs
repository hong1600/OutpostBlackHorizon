using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMng : MonoBehaviour
{
    public static event Action<Vector2> onLeftClick;

    public FieldSelector fieldSelector;
    public IFieldSelector iFieldSelector;

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
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onLeftClick?.Invoke(Input.mousePosition);
        }
    }
}
