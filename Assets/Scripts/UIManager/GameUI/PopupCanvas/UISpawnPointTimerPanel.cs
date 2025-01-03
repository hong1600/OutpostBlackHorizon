using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUISpawnPointTimerPanel
{
    GameObject GetSpawnPointTimerPanel();
    TextMeshProUGUI GetSpawnPointTimerText();
}

public class UISpawnPointTimerPanel : MonoBehaviour, IUISpawnPointTimerPanel
{
    public GameObject spawnPointTimerPanel;
    public TextMeshProUGUI spawnPointTimerText;

    public GameObject GetSpawnPointTimerPanel() { return spawnPointTimerPanel; }
    public TextMeshProUGUI GetSpawnPointTimerText() { return spawnPointTimerText; }
}
