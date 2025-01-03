using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRandomBtn : MonoBehaviour
{
    public GameObject randomPanel;

    public void ClickRandomBtn()
    {
        UIMng.instance.OpenPanel(randomPanel);
    }
}
