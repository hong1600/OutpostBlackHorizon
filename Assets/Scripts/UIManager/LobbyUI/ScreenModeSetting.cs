using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField] CustomDropdown dropDown;

    public void SetFullScreen(int _index)
    {
        if (_index == 0)
        {
            Screen.fullScreen = false;
        }
        else if (_index == 1)
        {
            Screen.fullScreen = true;
        }

        Resolution res = Screen.currentResolution;
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}

