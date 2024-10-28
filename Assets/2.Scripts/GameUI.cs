using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
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
    [SerializeField] GameObject spawnTimerPanel;
    [SerializeField] TextMeshProUGUI spawntimerText;

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
        roundText.text = $"WAVE {GameManager.Instance.CurRound.ToString()}";

        int min = GameManager.Instance.Min;
        float sec = GameManager.Instance.Sec;
        timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);

        monsterCountSlider.value = (float)GameManager.Instance.CurMonster / (float)GameManager.Instance.MaxMonster;
        monsterCountText.text = $"{GameManager.Instance.CurMonster} / {GameManager.Instance.MaxMonster}";

        warningText.text = $"{GameManager.Instance.CurMonster} / {GameManager.Instance.MaxMonster}";

        mainGold.text = GameManager.Instance.Gold.ToString();
        spawnGoldText.text = GameManager.Instance.SpawnGold.ToString();
        coinText.text = GameManager.Instance.Coin.ToString();
        UnitCountText.text = $"{GameManager.Instance.UnitCount.ToString()} / 20";

        if (sec < 3.4)
        {
            spawnTimerPanel.SetActive(true);
            spawntimerText.text = sec.ToString();
        }
        else if (sec < 0)
        {
            spawnTimerPanel.SetActive(false);
        }
    }

    private void upgradePanel()
    {
        upgradeGoldText.text = GameManager.Instance.Gold.ToString();
        upgradeCoinText.text = GameManager.Instance.Coin.ToString();
        upgradeCost1Text.text = GameManager.Instance.UpgradeCost1.ToString();
        upgradeCost2Text.text = GameManager.Instance.UpgradeCost2.ToString();
        upgradeCost3Text.text = GameManager.Instance.UpgradeCost3.ToString();
        upgradeCost4Text.text = GameManager.Instance.UpgradeCost4.ToString();
        if (GameManager.Instance.UpgradeLevel1 < 6)
        {
            upgradeLevel1Text.text = "LV." + GameManager.Instance.UpgradeLevel1.ToString();
        }
        else
        {
            upgradeLevel1Text.text = "LV.MAX";
        }
        if (GameManager.Instance.UpgradeLevel2 < 6)
        {
            upgradeLevel2Text.text = "LV." + GameManager.Instance.UpgradeLevel2.ToString();
        }
        else
        {
            upgradeLevel2Text.text = "LV.MAX";
        }
        if (GameManager.Instance.UpgradeLevel3 < 6)
        {
            upgradeLevel3Text.text = "LV." + GameManager.Instance.UpgradeLevel3.ToString();
        }
        else
        {
            upgradeLevel3Text.text = "LV.MAX";
        }
        if (GameManager.Instance.UpgradeLevel4 < 6)
        {
            upgradeLevel4Text.text = "LV." + GameManager.Instance.UpgradeLevel4.ToString();
        }
        else
        {
            upgradeLevel4Text.text = "LV.MAX";
        }
        spawnPerText1.text = 
            $"ÀÏ¹Ý : {GameManager.Instance.FirstSelectWeight[(int)GameManager.Instance.UpgradeLevel4 - 1][3]}%";
        spawnPerText2.text = 
            $"<color=blue>Èñ±Í : {GameManager.Instance.FirstSelectWeight[(int)GameManager.Instance.UpgradeLevel4 - 1][2]}%</color>%";
        spawnPerText3.text = 
            $"<color=purple>¿µ¿õ : {GameManager.Instance.FirstSelectWeight[(int)GameManager.Instance.UpgradeLevel4 - 1][1]}%</color>";
        spawnPerText4.text =
            $"<color=yellow>Àü¼³ : {GameManager.Instance.FirstSelectWeight[(int)GameManager.Instance.UpgradeLevel4 - 1][0]}%</color>";
    }

    private void randomPanel()
    {
        myCoinText.text = GameManager.Instance.Coin.ToString();
    }

    public void mixPanel(int index)
    {
        UnitData curUnit = mixUnitData[index];
        mixUnitNameText.text = mixUnitData[index].unitName;
        mixUnitImg1.sprite = mixUnitData[index].unitImg;
        mixUnitImg2.sprite = mixUnitData[index].unitImg;
    }

    private void warning()
    {
        if (GameManager.Instance.CurMonster >= GameManager.Instance.MaxMonster * 0.8f && checkWarning == true)
        {
            StartCoroutine(Warning());
            checkWarning = false;
        }
        if (GameManager.Instance.CurMonster < GameManager.Instance.MaxMonster * 0.8f)
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
