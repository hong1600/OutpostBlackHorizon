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
    [SerializeField] TextMeshProUGUI enemyCountText;
    [SerializeField] Image sliderValue;

    private void Start()
    {
        Shared.enemyMng.iEnemySpawner.UnEnemySpawn(EnemyCounterPanel);
        Shared.enemyMng.iEnemySpawner.SubEnemySpawn(EnemyCounterPanel);

        sliderValue.fillAmount = 0;
    }

    public void EnemyCounterPanel()
    {

        enemyCountText.text = $"{Shared.enemyMng.EnemyCount()}  /  {Shared.enemyMng.maxEnemyCount}";
        sliderValue.fillAmount = Shared.enemyMng.EnemyCount() / Shared.enemyMng.maxEnemyCount;
    }
}
