using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] Canvas worldCanvas;

    [SerializeField] UIMixRightSlot uiMixRightSlot;
    [SerializeField] UIFusionBtn uiFusionBtn;
    public IUIMixRightSlot iUIMixRightSlot;
    public IUIFusionBtn iUIFusionBtn;

    private void Awake()
    {
        if (Shared.gameUI == null)
        {
            Shared.gameUI = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        iUIMixRightSlot = uiMixRightSlot;
        iUIFusionBtn = uiFusionBtn;
    }
}
