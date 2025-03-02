using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] Canvas worldCanvas;

    [SerializeField] UIMixRightSlot uiMixRightSlot;
    [SerializeField] UIFusionBtn uiFusionBtn;

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

        UIMixRightSlot = uiMixRightSlot;
        UIFusionBtn = uiFusionBtn;
    }

    public UIMixRightSlot UIMixRightSlot { get; private set; }
    public UIFusionBtn UIFusionBtn { get; private set; }
}
