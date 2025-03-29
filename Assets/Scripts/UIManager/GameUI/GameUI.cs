using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public EScene eScene { get; private set; } = EScene.GAME;

    [Header("COMPONENTS")]
    [SerializeField] UIMixRightSlot uiMixRightSlot;
    [SerializeField] UIFusionBtn uiFusionBtn;
    [SerializeField] GameObject hitAim;

    [Header("BLACK OUT")]
    [SerializeField] Image blackImg;

    [Header("SWITCH")]
    [SerializeField] List<GameObject> fpsUI;
    [SerializeField] List<GameObject> topUI;
    [SerializeField] List<GameObject> NoneUI;

    [Header("SCOPE")]
    [SerializeField] GameObject scopeObj;
    public GameObject scope { get { return scopeObj; } }

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

    public IEnumerator StartBlackout(float _duration)
    {
        UIManager.instance.SetAlpha(blackImg, 1);

        yield return new WaitForSeconds(_duration);

        UIManager.instance.SetAlpha(blackImg, 0);
    }

    public void SwitchFps()
    {
        for (int i = 0; i < topUI.Count; i++)
        {
            topUI[i].SetActive(false);
        }
        for (int i = 0; i < fpsUI.Count; i++)
        {
            fpsUI[i].SetActive(true);
        }
    }

    public void SwitchTop()
    {
        for (int i = 0; i < fpsUI.Count; i++)
        {
            fpsUI[i].SetActive(false);
        }
        for (int i = 0; i < topUI.Count; i++)
        {
            topUI[i].SetActive(true);
        }
    }

    public void SwitchNone()
    {
        for (int i = 0; i < NoneUI.Count; i++)
        {
            NoneUI[i].SetActive(false);
        }
    }


    public UIFusionBtn UIFusionBtn { get; private set; }
    public UIMixRightSlot UIMixRightSlot { get; private set; }
    public GameObject HitAim { get; private set; }
}
