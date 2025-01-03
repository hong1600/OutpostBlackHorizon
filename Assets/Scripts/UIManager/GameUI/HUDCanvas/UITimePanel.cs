using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUITimePanel 
{
    void TimePanel();
}

public class UITimePanel : MonoBehaviour, IUITimePanel
{
    public TextMeshProUGUI timerText;

    public void TimePanel()
    {
        int min = (int)Shared.gameMng.iSpawnTimer.GetMin();
        float sec = Shared.gameMng.iSpawnTimer.GetSec();
        timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);
    }
}
