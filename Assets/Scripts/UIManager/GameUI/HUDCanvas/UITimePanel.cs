using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Image sliderValue;

    private void Start()
    {
        Shared.gameMng.SpawnTimer.UnTimerEvent(UpdateTimePanel);
        Shared.gameMng.SpawnTimer.SubTimerEvent(UpdateTimePanel);
    }

    private void UpdateTimePanel()
    {
        float sec = Shared.gameMng.SpawnTimer.GetSec();
        float maxSec = Shared.gameMng.SpawnTimer.GetMaxSec();
        timerText.text = $"{(int)sec}s";
        sliderValue.fillAmount = sec / maxSec;
        //timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);
    }
}
