using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    static EScene nextScene;

    [SerializeField] Image sliderValue;

    private void Start()
    {
        StartCoroutine(StartLoadScene());
    }

    public static void LoadScene(EScene _eScene)
    {
        SceneManager.LoadScene("Loading");
        nextScene = _eScene;
    }

    IEnumerator StartLoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync((int)nextScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if (sliderValue.fillAmount < 0.9f)
            {
                sliderValue.fillAmount = Mathf.MoveTowards(sliderValue.fillAmount, 0.9f, Time.deltaTime);
            }
            else if (sliderValue.fillAmount >= 0.9f)
            {
                sliderValue.fillAmount = Mathf.MoveTowards(sliderValue.fillAmount, 1f, Time.deltaTime);
            }
            if (sliderValue.fillAmount >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
