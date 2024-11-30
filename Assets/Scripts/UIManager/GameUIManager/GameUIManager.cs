using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public UITimePanel uiTimePanel;
    public IUITimePanel iUiTimePanel;
    public UIEnemyCounter uiEnemyCounter;
    public IUIEnemyCounter iUIEnemyCounter;
    public UIInGameMoneyPanel uiInGameMoneyPanel;
    public IUIInGameMoneyPanel iUIInGameMoneyPanel;

    private void Awake()
    {
        iUiTimePanel = uiTimePanel;
        iUIEnemyCounter = uiEnemyCounter;
        iUIInGameMoneyPanel = uiInGameMoneyPanel;
    }

    private void Update()
    {
        iUiTimePanel.timePanel();
        iUIEnemyCounter.enemyCounterPanel();
        iUIInGameMoneyPanel.inGameMoneyPanel();
    }

}
