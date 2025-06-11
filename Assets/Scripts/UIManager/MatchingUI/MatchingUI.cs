using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchingUI : Singleton<MatchingUI>
{
    [SerializeField] GameObject matchingPanel;
    [SerializeField] GameObject cancleBtn;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] Color color;

    bool isMatching = true;

    float elapsedTime;
    int time;
    int sec;
    int min;

    private void OnEnable()
    {
        elapsedTime = 0f;
        time = 0;

        PhotonManager.instance.PhotonMaching.StartMatching();
    }

    private void Update()
    {
        if (isMatching)
        {
            RunTimer();
        }
    }

    private void RunTimer()
    {
        elapsedTime += Time.deltaTime;

        time = (int)elapsedTime;
        min = time / 60;
        sec = time % 60;

        timerText.text = $"경과 시간: {min:00}:{sec:00}";
    }

    public void CompleteMatch()
    {
        stateText.text = "게임 발견!";
        timerText.text = "게임에 참가중";
        matchingPanel.GetComponent<Image>().color = color;
        cancleBtn.SetActive(false);
    }

    public void ClickCancleBtn()
    {
        this.gameObject.SetActive(false);
        PhotonManager.instance.PhotonMaching.CancleMatching();
    }
}
