using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI monsterCountText;
    [SerializeField] Slider monsterCountSlider;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] TextMeshProUGUI myGold;
    [SerializeField] TextMeshProUGUI spawnGoldText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI UnitCountText;

    private void Update()
    {
        timer();
        round();
        monsterSlider();
        warning();
        money();
    }

    private void round()
    {
        roundText.text = $"WAVE {GameManager.Instance.CurRound.ToString()}";
    }

    private void timer()
    {
        int min = GameManager.Instance.Min;
        float sec = GameManager.Instance.Sec;

        timerText.text = string.Format("{0:00}:{1:00}", min, (int)sec);
    }

    private void monsterSlider()
    {
        monsterCountSlider.value = (float)GameManager.Instance.CurMonster/(float)GameManager.Instance.MaxMonster;
        monsterCountText.text = $"{GameManager.Instance.CurMonster} / {GameManager.Instance.MaxMonster}";
    }

    private void warning()
    {
        warningText.text = $"{GameManager.Instance.CurMonster} / {GameManager.Instance.MaxMonster}";
    }

    private void money()
    {
        myGold.text = GameManager.Instance.MyGold.ToString();

        spawnGoldText.text = GameManager.Instance.SpawnGold.ToString();

        coinText.text = GameManager.Instance.Coin.ToString();

        UnitCountText.text = GameManager.Instance.UnitCount.ToString();
    }
}
