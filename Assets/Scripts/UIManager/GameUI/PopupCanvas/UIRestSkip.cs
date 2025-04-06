using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRestSkip : MonoBehaviour
{
    Timer timer;

    [SerializeField] GameObject timerDescText;
    [SerializeField] GameObject restSkipBtn;

    private void Start()
    {
        timer = GameManager.instance.Timer;
        timer.onRestTime += ShowBtn;
    }
    public void ClickSkipRestTime()
    {
        timer.SetSec(0.1f);
        restSkipBtn.SetActive(false);
        timerDescText.SetActive(false);
    }

    private void ShowBtn()
    {
        restSkipBtn.SetActive(true);
        timerDescText.SetActive(true);
    }

}
