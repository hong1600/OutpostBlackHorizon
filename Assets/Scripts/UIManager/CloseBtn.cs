using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    public void clickCloseBtn()
    {
        UIMng.instance.ClosePanel(transform.parent.gameObject);
    }
}
