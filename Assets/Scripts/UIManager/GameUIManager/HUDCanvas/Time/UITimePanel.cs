using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUITimePanel 
{
    void timePanel();
}

public class UITimePanel : MonoBehaviour, IUITimePanel
{
    public Timer timer;
    public ITimer iTimer;

    public TextMeshProUGUI timerText;

    private void Awake()
    {
        iTimer = timer;
    }

    public void timePanel()
    {
        int min = (int)iTimer.getMin();
        float sec = iTimer.getSec();
        timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);
    }
}
