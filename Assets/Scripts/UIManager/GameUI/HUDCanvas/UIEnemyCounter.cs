using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemyCountText;
    [SerializeField] Image sliderValue;

    private void Start()
    {
        Shared.enemyManager.onEnemyCountEvent -= EnemyCounterPanel;
        Shared.enemyManager.onEnemyCountEvent += EnemyCounterPanel;

        enemyCountText.text = $"{Shared.enemyManager.GetCurEnemy()}  /  {Shared.enemyManager.GetMaxEnemy()}";
        sliderValue.fillAmount = 0;
    }

    private void EnemyCounterPanel()
    {
        float curEnemy = Shared.enemyManager.GetCurEnemy();
        float maxEnemy = Shared.enemyManager.GetMaxEnemy();

        enemyCountText.text = $"{curEnemy}  /  {maxEnemy}";
        sliderValue.fillAmount = curEnemy / maxEnemy;
    }
}
