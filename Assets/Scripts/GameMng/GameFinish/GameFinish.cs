using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    public Coroutine gameClear;

    public void ClearGame()
    {

    }

    IEnumerator GameFinsh()
    {
        //waringUI.gameOverPanel.SetActive(true);
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.5f);

        //waringUI.gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        Shared.sceneMng.ChangeScene(EScene.LOBBY, true);
    }
}
