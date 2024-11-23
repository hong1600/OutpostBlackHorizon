using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMix : MonoBehaviour
{
    public GameObject mixPanel;

    public void clickMixBtn()
    {
        mixPanel.SetActive(true);
    }
}
