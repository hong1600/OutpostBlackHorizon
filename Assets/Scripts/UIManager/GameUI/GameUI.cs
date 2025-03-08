using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] Canvas worldCanvas;

    [SerializeField] UIMixRightSlot uiMixRightSlot;
    [SerializeField] UIFusionBtn uiFusionBtn;
    [SerializeField] GameObject hitAim;
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
        HitAim = hitAim;
    }

    public UIMixRightSlot UIMixRightSlot { get; private set; }
    public UIFusionBtn UIFusionBtn { get; private set; }
    public GameObject HitAim { get; private set; }
}
