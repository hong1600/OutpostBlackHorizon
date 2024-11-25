using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIEnemyCounter : MonoBehaviour
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
        monsterCountSlider.value = (float)enemyMng.enemyCount() / (float)enemyMng.maxEnemyCount;
        monsterCountText.text = $"{enemyMng.curEnemyCount}  /  {enemyMng.maxEnemyCount}";
    }
}
