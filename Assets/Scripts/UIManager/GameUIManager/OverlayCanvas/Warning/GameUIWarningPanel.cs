using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIWarningPanel : MonoBehaviour
{
    public EnemyMng enemyMng;
    public IEnemyMng iEnemyMng;

    public GameObject warningPanel;
    public TextMeshProUGUI warningText;
    public bool isWarning;

    private void Awake()
    {
        iEnemyMng = enemyMng;
        isWarning = false;
    }

    private void warning()
    {
        if (iEnemyMng.enemyCount() >= iEnemyMng.getMaxEnemyCount() * 0.8f && isWarning == true)
        {
            StartCoroutine(Warning());
            isWarning = false;
        }
        if (iEnemyMng.enemyCount() < iEnemyMng.getMaxEnemyCount() * 0.8f)
        {
            isWarning = true;
        }

        warningText.text = $"{iEnemyMng.enemyCount()} / {iEnemyMng.getMaxEnemyCount()}";
    }

    IEnumerator Warning()
    {
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        warningPanel.SetActive(false);
    }
}
