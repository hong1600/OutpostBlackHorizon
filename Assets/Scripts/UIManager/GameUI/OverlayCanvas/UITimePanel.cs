using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimePanel : MonoBehaviour
{
    Timer timer;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Image sliderValue;
    [SerializeField] TextMeshProUGUI timerDescText;

    private void Start()
    {
        timer = GameManager.instance.Timer;
        timer.onTimeEvent += UpdateTimePanel;
        timer.onRestTime += UpdateRestText;
    }

    private void UpdateTimePanel()
    {
        float sec = timer.GetSec();
        float maxSec = timer.maxSec;
        timerText.text = $"{(int)sec}s";
        sliderValue.fillAmount = sec / maxSec;
    }

    private void UpdateRestText()
    {
        if (!timer.isSpawnTime)
        {
            timerDescText.text = "�� ��ġ�ϰ�, ������ �ϱ�";
        }
        else
        {
            timerDescText.text = "�������� ���� ����";
        }
    }
}
