using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradePerBtn : MonoBehaviour
{
    public GameObject spawnPerPanel;

    public void clickSpawnPer()
    {
        UIMng.instance.OpenPanel(spawnPerPanel);
    }
}
