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
        Shared.enemyMng.iEnemySpawner.UnEnemySpawn(EnemyCounterPanel);
        Shared.enemyMng.iEnemySpawner.SubEnemySpawn(EnemyCounterPanel);

        enemyCountText.text = $"{Shared.enemyMng.iEnemyMng.GetCurEnemy()}  /  {Shared.enemyMng.iEnemyMng.GetMaxEnemy()}";
        sliderValue.fillAmount = 0;
    }

    private void EnemyCounterPanel()
    {

        enemyCountText.text = $"{Shared.enemyMng.iEnemyMng.GetCurEnemy()}  /  {Shared.enemyMng.iEnemyMng.GetMaxEnemy()}";
        sliderValue.fillAmount = Shared.enemyMng.iEnemyMng.GetCurEnemy() / Shared.enemyMng.iEnemyMng.GetMaxEnemy();
    }
}
