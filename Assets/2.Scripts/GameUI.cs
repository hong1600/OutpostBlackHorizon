using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("메인")]
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
    bool checkWarning = true;

    [Header("업그레이드")]
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

    private void Update()
    {
        main();
        upgradePanel();
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
    }

    private void upgradePanel()
    {
        upgradeGoldText.text = GameManager.Instance.Gold.ToString();
        upgradeCoinText.text = GameManager.Instance.Coin.ToString();
        upgradeCost1Text.text = GameManager.Instance.UpgradeCost1.ToString();
        upgradeCost2Text.text = GameManager.Instance.UpgradeCost2.ToString();
        upgradeCost3Text.text = GameManager.Instance.UpgradeCost3.ToString();
        upgradeCost4Text.text = GameManager.Instance.UpgradeCost4.ToString();
        upgradeLevel1Text.text = "LV." + GameManager.Instance.UpgradeLevel1.ToString();
        upgradeLevel2Text.text = "LV." + GameManager.Instance.UpgradeLevel2.ToString();
        upgradeLevel3Text.text = "LV." + GameManager.Instance.UpgradeLevel3.ToString();
        upgradeLevel4Text.text = "LV." + GameManager.Instance.UpgradeLevel4.ToString();
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
