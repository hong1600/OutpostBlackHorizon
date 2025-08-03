using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualitySetting : MonoBehaviour
{
    List<string> qualOptionList = new List<string>()
    {
        "����", "�߰�", "����", "�ſ����"
    };

    private void Start()
    {
        QualitySettings.SetQualityLevel(3);

        int curQual = QualitySettings.GetQualityLevel() -1;
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
