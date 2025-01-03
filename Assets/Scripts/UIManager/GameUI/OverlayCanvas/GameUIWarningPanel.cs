using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIWarningPanel : MonoBehaviour
{
    public GameObject warningPanel;
    public TextMeshProUGUI warningText;
    public bool isWarning;

    private void Awake()
    {
        isWarning = false;
    }

    private void Warning()
    {
        if (Shared.enemyMng.EnemyCount() >= Shared.enemyMng.maxEnemyCount * 0.8f && isWarning == true)
        {
            StartCoroutine(StartWarning());
            isWarning = false;
        }
        if (Shared.enemyMng.EnemyCount() < Shared.enemyMng.maxEnemyCount * 0.8f)
        {
            isWarning = true;
        }

        warningText.text = $"{Shared.enemyMng.EnemyCount()} / {Shared.enemyMng.maxEnemyCount}";
    }

    IEnumerator StartWarning()
    {
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        warningPanel.SetActive(false);
    }
}
