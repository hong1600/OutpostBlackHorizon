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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            timer.SetSec(0.1f);
            restSkipBtn.SetActive(false);
            timerDescText.SetActive(false);
        }
    }

    public void ClickSkipRestTime()
    {
        timer.SetSec(0.1f);
        restSkipBtn.SetActive(false);
        timerDescText.SetActive(false);
    }

    private void ShowBtn()
    {
        if (timer.isRestTime)
        {
            restSkipBtn.SetActive(true);
            timerDescText.SetActive(true);
        }
        else
        {
            restSkipBtn.SetActive(false);
        }
    }

}
