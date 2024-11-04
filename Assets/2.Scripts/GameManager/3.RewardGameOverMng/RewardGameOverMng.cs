using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RewardGameOverMng : MonoBehaviour
{
    private GameManager gameManager;

    public float rewardGold;
    public float rewardGem;
    public float rewardPaper;
    public float rewardExp;
    public bool gameOver;
    public bool gameClear;


    public RewardGameOverMng(GameManager manager)
    {
        gameManager = manager;

        gameClear = false;
        gameOver = false;
    }

    public void checkGameOver()
    {
        if (GameManager.Instance.enemyMng.curEnemyCount >= GameManager.Instance.enemyMng.maxEnemyCount
            || gameOver == true)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        GameUI.instance.gameOverPaenl.SetActive(true);
        Time.timeScale = 0.5f;
        gameOver = true;

        yield return new WaitForSeconds(1.5f);

        GameUI.instance.gameOverPaenl.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }

    public void clearGame()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator ClearGame()
    {
        GameUI.instance.gameOverPaenl.SetActive(true);
        Time.timeScale = 0.5f;
        gameClear = true;

        yield return new WaitForSeconds(1.5f);

        GameUI.instance.gameOverPaenl.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

}
