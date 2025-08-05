using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField] CustomDropdown dropdown;
    [SerializeField] ResolutionSetting resolutionSetting;

    [SerializeField] TextMeshProUGUI labelText;

    private void Start()
    {
        labelText.text = "전체화면";

        dropdown.onOptionSelectedEvent.AddListener(SetFullScreen);
    }

    public void SetFullScreen(int _index)
    {
        Debug.Log(_index);

        if (_index == 0)
        {
            Screen.fullScreen = false;
        }
        else if (_index == 1)
        {
            Screen.fullScreen = true;
        }

        StartCoroutine(ApplyResolutionAfterFullscreenChange());
    }

    IEnumerator ApplyResolutionAfterFullscreenChange()
    {
        yield return null;
        resolutionSetting.ReapplyCurrentResolution();
    }
}

