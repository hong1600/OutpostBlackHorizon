using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIWarningPanel : MonoBehaviour
{
    [SerializeField] GameObject warningPanel;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] bool isWarning;

    private void Awake()
    {
        isWarning = false;
    }

    private void Warning()
    {
        if (Shared.enemyMng.iEnemyMng.GetCurEnemy() >= Shared.enemyMng.iEnemyMng.GetMaxEnemy() * 0.8f && isWarning == true)
        {
            StartCoroutine(StartWarning());
            isWarning = false;
        }
        if (Shared.enemyMng.iEnemyMng.GetCurEnemy() < Shared.enemyMng.iEnemyMng.GetMaxEnemy() * 0.8f)
        {
            isWarning = true;
        }

        warningText.text = $"{Shared.enemyMng.iEnemyMng.GetCurEnemy()} / {Shared.enemyMng.iEnemyMng.GetMaxEnemy()}";
    }

    IEnumerator StartWarning()
    {
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        warningPanel.SetActive(false);
    }
}
