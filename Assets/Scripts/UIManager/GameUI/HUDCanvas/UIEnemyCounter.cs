using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IUIEnemyCounter
{
    void EnemyCounterPanel();
}

public class UIEnemyCounter : MonoBehaviour, IUIEnemyCounter
{
    public TextMeshProUGUI monsterCountText;
    public Slider monsterCountSlider;

    public void EnemyCounterPanel()
    {
        monsterCountSlider.value = Shared.enemyMng.EnemyCount() / Shared.enemyMng.maxEnemyCount;
        monsterCountText.text = $"{Shared.enemyMng.EnemyCount()}  /  {Shared.enemyMng.maxEnemyCount}";
    }
}
