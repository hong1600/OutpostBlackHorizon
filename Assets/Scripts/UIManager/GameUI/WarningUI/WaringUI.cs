using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaringUI : MonoBehaviour
{
    public EnemyMng enemyMng;

    public bool checkWarning = true;
    public GameObject warningPanel;
    public TextMeshProUGUI warningText;
    public GameObject gameOverPanel;

    private void warning()
    {
        if (enemyMng.curEnemyCount >= enemyMng.maxEnemyCount * 0.8f && checkWarning == true)
        {
            StartCoroutine(Warning());
            checkWarning = false;
        }
        if (enemyMng.curEnemyCount < enemyMng.maxEnemyCount * 0.8f)
        {
            checkWarning = true;
        }

        warningText.text = $"{enemyMng.curEnemyCount} / {enemyMng.maxEnemyCount}";

    }

    IEnumerator Warning()
    {
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        warningPanel.SetActive(false);
    }
}
