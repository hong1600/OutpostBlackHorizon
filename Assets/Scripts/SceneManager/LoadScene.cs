using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public static int nextScene;

    [SerializeField] Slider slider;
    [SerializeField] GameObject loadingText;
    [SerializeField] GameObject pressText;

    private void Start()
    {
        StartCoroutine(loadScene());
    }

    public static void loadScene(int sceneNum)
    {
        nextScene = sceneNum;
        SceneManager.LoadScene(3);
    }

    IEnumerator loadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
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
