using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUISpawnPointTimerPanel : MonoBehaviour
{
    public Round round;
    public IRound iRound;
    public Timer timer;
    public ITimer iTimer;

    public GameObject spawnPointTimerPanel;
    public TextMeshProUGUI spawnPointTimerText;

    private void Awake()
    {
        iRound = round;
        iTimer = timer;
    }

    public void spawnPointTimer()
    {
        if (iRound.isBossRound())
        {
            spawnPointTimerPanel.SetActive(false);
        }
        if (iTimer.getSec() < 4 && spawnPointTimerPanel.activeSelf
            && !iRound.isBossRound())
        {
            spawnPointTimerPanel.SetActive(true);
        }

        int intsec = (int)iTimer.getSec();
        spawnPointTimerText.text = intsec.ToString();
    }
}
