using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionBtn : MonoBehaviour
{
    public void ClickOption()
    {
        UIManager.instance.Option.OpenOption();
    }
}
