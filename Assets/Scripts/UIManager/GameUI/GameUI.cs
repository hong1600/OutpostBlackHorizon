using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    public EScene eScene { get; private set; } = EScene.GAME;

    [Header("BlackOut")]
    [SerializeField] Image blackImg;

    [Header("Switch")]
    [SerializeField] List<GameObject> fpsUI;
    [SerializeField] List<GameObject> topUI;
    [SerializeField] List<GameObject> NoneUI;

    [Header("Scope")]
    [SerializeField] GameObject scopeObj;
    [SerializeField] GameObject hitAim;

    [Header("Components")]
    [SerializeField] UIFusionBtn uiFusionBtn;
    [SerializeField] UIMixRightSlot uiMixRightSlot;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameObject cursor = UIManager.instance.Cursor;
        topUI.Add(cursor);
        NoneUI.Add(cursor);
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

    public UIFusionBtn UIFusionBtn { get { return uiFusionBtn; } }
    public UIMixRightSlot UIMixRightSlot { get { return uiMixRightSlot; } }
    public GameObject HitAim { get { return hitAim; } }
    public GameObject scope { get { return scopeObj; } }
}
