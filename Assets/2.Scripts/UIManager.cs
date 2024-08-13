using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI monsterCountText;
    [SerializeField] Slider monsterCountSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        timer();
        round();
        monsterSlider();
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
}
