using DG.Tweening;
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

        sliderValue.DOFillAmount(sec / maxSec, 0.2f).SetEase(Ease.Linear);
    }

    //IEnumerator StartSmoothSilder(float _targetValue)
    //{
    //    float duration = 0.2f;
    //    float elapsed = 0f;
    //    float startValue = sliderValue.fillAmount;

    //    while (elapsed < duration) 
    //    {
    //        elapsed += Time.deltaTime;
    //        sliderValue.fillAmount = Mathf.Lerp(startValue, _targetValue, elapsed / duration);
    //        yield return null;
    //    }

    //    sliderValue.fillAmount = _targetValue;
    //}

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
