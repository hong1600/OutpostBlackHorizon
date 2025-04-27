using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapSelectBtn : MonoBehaviour
{
    [SerializeField] WaitingUI waitingUI;

    public void ClickNextMap(int _index)
    {
        waitingUI.UpdateMap(_index);
    }

    public void ClickPrevMap(int _index)
    {
        waitingUI.UpdateMap(_index);
    }
}
