using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBtn : BtnManager
{
    public UnitMixer unitMixer;

    public GameObject mixPanel;

    public void mixBtn()
    {
        openPanel(mixPanel);
        unitMixer.unitCanMix();
    }

    public void mixUnitSpawnBtn()
    {
        unitMixer.unitMixSpawn();
    }
}
