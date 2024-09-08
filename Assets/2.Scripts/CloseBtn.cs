using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    public void closeBtn(GameObject button)
    {
        if (button.transform.parent != null)
        {
            button.transform.parent.gameObject.SetActive(false);
        }
    }
}
