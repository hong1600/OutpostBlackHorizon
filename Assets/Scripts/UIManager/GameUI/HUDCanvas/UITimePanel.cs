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

    private void Start()
    {
        timer = Shared.gameManager.Timer;
        timer.onTimeEvent += UpdateTimePanel;
    }

    private void UpdateTimePanel()
    {
        float sec = timer.GetSec();
        float maxSec = timer.maxSec;
        timerText.text = $"{(int)sec}s";
        sliderValue.fillAmount = sec / maxSec;
    }
}
