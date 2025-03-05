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
        Shared.enemyManager.iEnemyMng.onEnemyCountEvent -= EnemyCounterPanel;
        Shared.enemyManager.iEnemyMng.onEnemyCountEvent += EnemyCounterPanel;

        enemyCountText.text = $"{Shared.enemyManager.iEnemyMng.GetCurEnemy()}  /  {Shared.enemyManager.iEnemyMng.GetMaxEnemy()}";
        sliderValue.fillAmount = 0;
    }

    private void EnemyCounterPanel()
    {
        float curEnemy = Shared.enemyManager.iEnemyMng.GetCurEnemy();
        float maxEnemy = Shared.enemyManager.iEnemyMng.GetMaxEnemy();

        enemyCountText.text = $"{curEnemy}  /  {maxEnemy}";
        sliderValue.fillAmount = curEnemy / maxEnemy;
    }
}
