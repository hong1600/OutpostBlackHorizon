using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField] TMP_Dropdown modeDropdown;
    [SerializeField] TextMeshProUGUI modeText;

    List<string> modeList = new List<string> { "창 모드", "전체 화면"};

    private void Start()
    {
        modeDropdown.options.Clear();
        modeDropdown.AddOptions(modeList);
        Screen.fullScreen = true;
        modeDropdown.value = Screen.fullScreen ? 1 : 0;
        SetFullScreen(modeDropdown.value);

        modeDropdown.onValueChanged.AddListener(SetFullScreen);
    }

    public void SetFullScreen(int _index)
    {
        if (_index == 0)
        {
            Screen.fullScreen = false;
            modeText.text = "창 모드";
        }
        else if (_index == 1)
        {
            Screen.fullScreen = true;
            modeText.text = "전체 화면";
        }

        Resolution res = Screen.currentResolution;
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
