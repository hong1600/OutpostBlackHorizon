using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    public EScene eScene { get; private set; } = EScene.GAME;

    [SerializeField] List<GameObject> functionUI;

    [Header("BlackOut")]
    [SerializeField] Image blackImg;

    [Header("Switch")]
    [SerializeField] List<GameObject> fpsUI;
    [SerializeField] List<GameObject> topUI;
    [SerializeField] List<GameObject> turretUI;
    [SerializeField] GameObject restTimePanel;

    [Header("Scope")]
    [SerializeField] GameObject scopeObj;
    [SerializeField] GameObject hitAim;

    [Header("Components")]
    [SerializeField] UIFusionBtn uiFusionBtn;
    [SerializeField] UIMixRightSlot uiMixRightSlot;
    [SerializeField] UIBuild uiBuild;
    [SerializeField] UIBossHpbar uiBossHpbar;
    [SerializeField] UIInteraction uiInteraction;
    [SerializeField] UIGameChat uiGameChat;

    [Header("Canvas")]
    [SerializeField] Transform skillBarParent;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameObject cursor = UIManager.instance.Cursor;
        topUI.Add(cursor);
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

        if (!EnemyManager.instance.EnemySpawner.IsSpawn)
        {
            restTimePanel.SetActive(true);
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

        if (!EnemyManager.instance.EnemySpawner.IsSpawn)
        {
            restTimePanel.SetActive(true);
        }
    }

    public void SwitchTurret()
    {
        for (int i = 0; i < fpsUI.Count; i++)
        {
            fpsUI[i].SetActive(false);
        }
        for (int i = 0; i < topUI.Count; i++)
        {
            topUI[i].SetActive(false);
        }
        for (int i = 0; i < turretUI.Count; i++)
        {
            turretUI[i].SetActive(true);
        }
    }

    public void SwitchNone()
    {
        for (int i = 0; i < fpsUI.Count; i++)
        {
            fpsUI[i].SetActive(false);
        }
        for (int i = 0; i < topUI.Count; i++)
        {
            topUI[i].SetActive(false);
        }
        restTimePanel.SetActive(false);
    }

    public Transform SkillBarParent { get { return skillBarParent; } }
    public UIFusionBtn UIFusionBtn { get { return uiFusionBtn; } }
    public UIMixRightSlot UIMixRightSlot { get { return uiMixRightSlot; } }
    public UIBuild UIBuild { get { return uiBuild; } }
    public UIBossHpbar UIBossHpbar { get { return uiBossHpbar; } }
    public UIInteraction UIInteraction { get { return uiInteraction; } }
    public UIGameChat UIGameChat { get { return uiGameChat; } }
    public GameObject HitAim { get { return hitAim; } }
    public GameObject scope { get { return scopeObj; } }
}
