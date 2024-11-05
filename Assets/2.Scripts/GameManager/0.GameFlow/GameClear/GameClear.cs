using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    public GameFlow gameFlow;

    public Coroutine gameClear;

    public void initialized(GameFlow manager)
    {
        gameFlow = manager;
    }

    public void clearGame()
    {
    }

    IEnumerator GameFinsh()
    {
        GameUI.instance.gameOverPaenl.SetActive(true);
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.5f);

        GameUI.instance.gameOverPaenl.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }

}
