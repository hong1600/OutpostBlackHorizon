using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradePerBtn : MonoBehaviour
{
    [SerializeField] GameObject spawnPerPanel;

    public void clickSpawnPer()
    {
        UIMng.instance.OpenPanel(spawnPerPanel, null, null, false);
    }
}
