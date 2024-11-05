using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    [Header("main")]
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI monsterCountText;
    [SerializeField] Slider monsterCountSlider;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] TextMeshProUGUI mainGold;
    [SerializeField] TextMeshProUGUI spawnGoldText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI UnitCountText;
    [SerializeField] GameObject warningPanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TextMeshProUGUI waveBossLevelNameText;
    [SerializeField] TextMeshProUGUI bossTimer;
    bool checkWarning = true;

    [Header("upgrade")]
    [SerializeField] TextMeshProUGUI upgradeGoldText;
    [SerializeField] TextMeshProUGUI upgradeCoinText;
    [SerializeField] TextMeshProUGUI upgradeCost1Text;
    [SerializeField] TextMeshProUGUI upgradeCost2Text;
    [SerializeField] TextMeshProUGUI upgradeCost3Text;
    [SerializeField] TextMeshProUGUI upgradeCost4Text;
    [SerializeField] TextMeshProUGUI upgradeLevel1Text;
    [SerializeField] TextMeshProUGUI upgradeLevel2Text;
    [SerializeField] TextMeshProUGUI upgradeLevel3Text;
    [SerializeField] TextMeshProUGUI upgradeLevel4Text;
    [SerializeField] TextMeshProUGUI spawnPerText1;
    [SerializeField] TextMeshProUGUI spawnPerText2;
    [SerializeField] TextMeshProUGUI spawnPerText3;
    [SerializeField] TextMeshProUGUI spawnPerText4;

    [Header("Random")]
    [SerializeField] TextMeshProUGUI myCoinText;

    [Header("Mix")]
    [SerializeField] TextMeshProUGUI mixUnitNameText;
    [SerializeField] Image mixUnitImg1;
    [SerializeField] Image mixUnitImg2;
    [SerializeField] UnitData[] mixUnitData;
    [SerializeField] GameObject canMixBtn;

    public GameObject spawnPointTimerPanel;
    public Text spawnPointTimerText;
    public GameObject spawnWaveBossBtn;
    public GameObject mixPanel;
    public GameObject gameOverPaenl;

    private void Start()
    {
        UnitData curUnit = mixUnitData[0];
        mixUnitNameText.text = mixUnitData[0].unitName;
        mixUnitImg1.sprite = mixUnitData[0].unitImg;
        mixUnitImg2.sprite = mixUnitData[0].unitImg;
    }

    private void Update()
    {
        main();
        upgradePanel();
        randomPanel();
        warning();
    }

    private void main()
    {
        roundText.text = $"WAVE {GameManager.Instance.gameFlow.roundTimer.curRound.ToString()}";

        int min = (int)GameManager.Instance.gameFlow.roundTimer.min;
        float sec = GameManager.Instance.gameFlow.roundTimer.sec;
        timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);

        monsterCountSlider.value = (float)GameManager.Instance.enemyMng.enemyCount() / (float)GameManager.Instance.enemyMng.maxEnemyCount;
        monsterCountText.text = $"{GameManager.Instance.enemyMng.curEnemyCount} / {GameManager.Instance.enemyMng.maxEnemyCount}";

        warningText.text = $"{GameManager.Instance.enemyMng.curEnemyCount} / {GameManager.Instance.enemyMng.maxEnemyCount}";

        mainGold.text = GameManager.Instance.myGold.ToString();
        spawnGoldText.text = GameManager.Instance.unitMng.spawnGold.ToString();
        coinText.text = GameManager.Instance.myCoin.ToString();
        UnitCountText.text = $"{GameManager.Instance.unitMng.curUnitList.Count.ToString()} / 20";

        waveBossLevelNameText.text = $"LV.{GameManager.Instance.enemyMng.waveBossSpawner.waveBossLevel} µ¹ °ñ·½";

        if (GameManager.Instance.gameFlow.roundTimer.bossRound)
        {
            spawnPointTimerPanel.SetActive(false);
        }
        if (GameManager.Instance.gameFlow.roundTimer.sec < 4 && spawnPointTimerPanel.activeSelf 
            && !GameManager.Instance.gameFlow.roundTimer.bossRound)
        {
            spawnPointTimerPanel.SetActive(true);
        }

        int intsec = (int)GameManager.Instance.gameFlow.roundTimer.sec;
        spawnPointTimerText.text = intsec.ToString();
    }

    private void upgradePanel()
    {
        upgradeGoldText.text = GameManager.Instance.myGold.ToString();
        upgradeCoinText.text = GameManager.Instance.myCoin.ToString();
        upgradeCost1Text.text = GameManager.Instance.unitMng.unitUpgrader.upgradeCost1.ToString();
        upgradeCost2Text.text = GameManager.Instance.unitMng.unitUpgrader.upgradeCost2.ToString();
        upgradeCost3Text.text = GameManager.Instance.unitMng.unitUpgrader.upgradeCost3.ToString();
        upgradeCost4Text.text = GameManager.Instance.unitMng.unitUpgrader.upgradeCost4.ToString();
        if (GameManager.Instance.unitMng.unitUpgrader.upgradeLevel1 < 6)
        {
            upgradeLevel1Text.text = "LV." + GameManager.Instance.unitMng.unitUpgrader.upgradeLevel1.ToString();
        }
        else
        {
            upgradeLevel1Text.text = "LV.MAX";
        }
        if (GameManager.Instance.unitMng.unitUpgrader.upgradeLevel2 < 6)
        {
            upgradeLevel2Text.text = "LV." + GameManager.Instance.unitMng.unitUpgrader.upgradeLevel2.ToString();
        }
        else
        {
            upgradeLevel2Text.text = "LV.MAX";
        }
        if (GameManager.Instance.unitMng.unitUpgrader.upgradeLevel3 < 6)
        {
            upgradeLevel3Text.text = "LV." + GameManager.Instance.unitMng.unitUpgrader.upgradeLevel3.ToString();
        }
        else
        {
            upgradeLevel3Text.text = "LV.MAX";
        }
        if (GameManager.Instance.unitMng.unitUpgrader.upgradeLevel4 < 6)
        {
            upgradeLevel4Text.text = "LV." + GameManager.Instance.unitMng.unitUpgrader.upgradeLevel4.ToString();
        }
        else
        {
            upgradeLevel4Text.text = "LV.MAX";
        }
        spawnPerText1.text = 
            $"ÀÏ¹Ý : {GameManager.Instance.unitMng.unitSpawner.firstSelectWeight[(int)GameManager.Instance.unitMng.unitUpgrader.upgradeLevel4 - 1][3]}%";
        spawnPerText2.text = 
            $"<color=blue>Èñ±Í : {GameManager.Instance.unitMng.unitSpawner.firstSelectWeight[(int)GameManager.Instance.unitMng.unitUpgrader.upgradeLevel4 - 1][2]}%</color>%";
        spawnPerText3.text = 
            $"<color=purple>¿µ¿õ : {GameManager.Instance.unitMng.unitSpawner.firstSelectWeight[(int)GameManager.Instance.unitMng.unitUpgrader.upgradeLevel4 - 1][1]}%</color>";
        spawnPerText4.text =
            $"<color=yellow>Àü¼³ : {GameManager.Instance.unitMng.unitSpawner.firstSelectWeight[(int)GameManager.Instance.unitMng.unitUpgrader.upgradeLevel4 - 1][0]}%</color>";
    }

    private void randomPanel()
    {
        myCoinText.text = GameManager.Instance.myCoin.ToString();
    }

    public void curMixPanel(int index)
    {
        UnitData curUnit = mixUnitData[index];
        mixUnitNameText.text = mixUnitData[index].unitName;
        mixUnitImg1.sprite = mixUnitData[index].unitImg;
        mixUnitImg2.sprite = mixUnitData[index].unitImg;
    }

    private void warning()
    {
        if (GameManager.Instance.enemyMng.curEnemyCount >= GameManager.Instance.enemyMng.maxEnemyCount * 0.8f && checkWarning == true)
        {
            StartCoroutine(Warning());
            checkWarning = false;
        }
        if (GameManager.Instance.enemyMng.curEnemyCount < GameManager.Instance.enemyMng.maxEnemyCount * 0.8f)
        {
            checkWarning = true;
        }
    }

    IEnumerator Warning()
    {
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        warningPanel.SetActive(false);
    }

}
