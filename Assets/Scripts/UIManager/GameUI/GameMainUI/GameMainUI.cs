using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMainUI : MonoBehaviour
{
    public UnitSpawner unitSpawner;
    public UnitMng unitMng;
    public EnemyMng enemyMng;

    public IGoldCoin iGoldCoin;
    public IRound iRound;
    public ITimer iTimer;

    public TextMeshProUGUI mainGold;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI spawnGoldText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI monsterCountText;
    public Slider monsterCountSlider;
    public TextMeshProUGUI UnitCountText;
    public GameObject spawnPointTimerPanel;
    public Text spawnPointTimerText;

    private void main()
    {
        roundText.text = $"WAVE {iRound.getCurRound().ToString()}";

        int min = (int)iTimer.getMin();
        float sec = iTimer.getSec();
        timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);

        monsterCountSlider.value = (float)enemyMng.enemyCount() / (float)enemyMng.maxEnemyCount;
        monsterCountText.text = $"{enemyMng.curEnemyCount}  /  {enemyMng.maxEnemyCount}";

        mainGold.text = iGoldCoin.getGold().ToString();
        spawnGoldText.text = unitSpawner.spawnGold.ToString();
        coinText.text = iGoldCoin.getCoin().ToString();
        UnitCountText.text = $"{unitMng.curUnitList.Count.ToString()} / 20";


        if (iRound.isBossRound())
        {
            spawnPointTimerPanel.SetActive(false);
        }
        if (iTimer.getSec() < 4 && spawnPointTimerPanel.activeSelf
            && !iRound.isBossRound())
        {
            spawnPointTimerPanel.SetActive(true);
        }

        int intsec = (int)iTimer.getSec();
        spawnPointTimerText.text = intsec.ToString();
    }
}
