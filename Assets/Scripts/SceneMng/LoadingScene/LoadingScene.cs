using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public static EScene nextScene;

    public Slider slider;
    public GameObject loadingText;
    public GameObject pressText;

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

            if (slider.value < 0.9f)
            {
                slider.value = Mathf.MoveTowards(slider.value, 0.9f, Time.deltaTime);
            }
            else if (slider.value >= 0.9f)
            {
                slider.value = Mathf.MoveTowards(slider.value, 1f, Time.deltaTime);
            }

            if (slider.value >= 1f)
            {
                loadingText.SetActive(false);
                pressText.SetActive(true);
            }
            if (slider.value >= 1f && operation.progress >= 0.9f && Input.anyKeyDown)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
