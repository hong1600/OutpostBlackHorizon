using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMainUI : MonoBehaviour
{
    public RoundTimer roundTimer;
    public UnitSpawner unitSpawner;
    public UnitMng unitMng;
    public EnemyMng enemyMng;

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
        roundText.text = $"WAVE {roundTimer.curRound.ToString()}";

        int min = (int)roundTimer.min;
        float sec = roundTimer.sec;
        timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);

        monsterCountSlider.value = (float)enemyMng.enemyCount() / (float)enemyMng.maxEnemyCount;
        monsterCountText.text = $"{enemyMng.curEnemyCount}  /  {enemyMng.maxEnemyCount}";

        mainGold.text = GameManager.Instance.myGold.ToString();
        spawnGoldText.text = unitSpawner.spawnGold.ToString();
        coinText.text = GameManager.Instance.myCoin.ToString();
        UnitCountText.text = $"{unitMng.curUnitList.Count.ToString()} / 20";


        if (roundTimer.bossRound)
        {
            spawnPointTimerPanel.SetActive(false);
        }
        if (roundTimer.sec < 4 && spawnPointTimerPanel.activeSelf
            && !roundTimer.bossRound)
        {
            spawnPointTimerPanel.SetActive(true);
        }

        int intsec = (int)roundTimer.sec;
        spawnPointTimerText.text = intsec.ToString();
    }
}
