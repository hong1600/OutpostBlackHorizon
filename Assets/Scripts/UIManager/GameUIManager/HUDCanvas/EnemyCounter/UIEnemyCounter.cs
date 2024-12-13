using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IUIEnemyCounter
{
    void enemyCounterPanel();
}

public class UIEnemyCounter : MonoBehaviour, IUIEnemyCounter
{
    public EnemyMng enemyMng;
    public IEnemyMng iEnemyMng;

    public TextMeshProUGUI monsterCountText;
    public Slider monsterCountSlider;

    private void Awake()
    {
        iEnemyMng = enemyMng;
    }

    public void enemyCounterPanel()
    {
        monsterCountSlider.value = iEnemyMng.enemyCount() / enemyMng.maxEnemyCount;
        monsterCountText.text = $"{iEnemyMng.enemyCount()}  /  {enemyMng.maxEnemyCount}";
    }
}
