using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualitySetting : MonoBehaviour
{
    [SerializeField] CustomDropdown dropdown;

    [SerializeField] TextMeshProUGUI labelText;

    List<string> qualOptionList = new List<string>()
    {
        "낮음", "중간", "높음", "매우높음"
    };

    private void Start()
    {
        labelText.text = qualOptionList[2];

        dropdown.onOptionSelectedEvent.AddListener(SetQuality);
    }

    public void SetQuality(int _index)
    {
        switch (_index) 
        {
            case 0:
                QualitySettings.SetQualityLevel(1);
                break;
            case 1:
                QualitySettings.SetQualityLevel(2);
                break;
            case 2:
                QualitySettings.SetQualityLevel(3);
                break;
            case 3:
                QualitySettings.SetQualityLevel(4);
                break;
        }
    }
}
