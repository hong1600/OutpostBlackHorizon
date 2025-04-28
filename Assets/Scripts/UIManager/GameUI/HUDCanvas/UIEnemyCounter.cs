using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyCounter : MonoBehaviour
{
    EnemyManager enemyManager;

    [SerializeField] TextMeshProUGUI enemyCountText;
    [SerializeField] Image sliderValue;

    private void Start()
    {
        enemyManager = EnemyManager.instance;
        ObjectPoolManager.instance.EnemyPool.onEnemyCount += UpdateEnemyCounter;

        enemyCountText.text = $"{enemyManager.GetCurEnemy()}  /  {enemyManager.GetMaxEnemy()}";
        sliderValue.fillAmount = 0;
    }

    public void UpdateEnemyCounter()
    {
        float curEnemy = enemyManager.GetCurEnemy();
        float maxEnemy = enemyManager.GetMaxEnemy();

        enemyCountText.text = $"{curEnemy}  /  {maxEnemy}";
        sliderValue.fillAmount = curEnemy / maxEnemy;
    }
}
