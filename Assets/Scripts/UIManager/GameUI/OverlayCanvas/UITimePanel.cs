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
            timerDescText.text = "방어를 배치하고, 재정비 하기";
        }
        else
        {
            timerDescText.text = "몰려오는 적을 막기";
        }
    }
}
