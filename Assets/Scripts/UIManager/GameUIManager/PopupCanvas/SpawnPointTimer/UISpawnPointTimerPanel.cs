using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUISpawnPointTimerPanel
{
    GameObject getSpawnPointTimerPanel();
    TextMeshProUGUI getSpawnPointTimerText();
}

public class UISpawnPointTimerPanel : MonoBehaviour, IUISpawnPointTimerPanel
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

    public GameObject getSpawnPointTimerPanel() { return spawnPointTimerPanel; }
    public TextMeshProUGUI getSpawnPointTimerText() { return spawnPointTimerText; }
}
