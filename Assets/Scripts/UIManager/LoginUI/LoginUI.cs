using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [SerializeField] RawImage videoImg;
    [SerializeField] GameObject textParent;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] Image img;
    [SerializeField] GameObject loginPanel;

    private void Start()
    {
        Color color = videoImg.color;
        color.a = 0.5f;
        videoImg.color = color;
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2);

        yield return StartCoroutine(StartFadeOut(videoImg, 3));

        textParent.SetActive(true);
        Shared.videoMng.NextVideo(EVideo.LOGIN);

        yield return new WaitForSeconds(2);

        StartCoroutine(StartFadeOut(text1, 2));
        StartCoroutine(StartFadeOut(img, 2));
        yield return StartCoroutine(StartFadeOut(text2, 2));

        Color color = videoImg.color;
        color.a = 0.05f;
        videoImg.color = color;
        loginPanel.SetActive(true);
    }

    IEnumerator StartFadeOut(Graphic _ui, float _duration)
    {
        Color _color = _ui.color;
        float startAlpha = _color.a;
        float time = 0f;

        while (time < _duration) 
        {
            time += Time.deltaTime;
            _color.a = Mathf.Lerp(startAlpha, 0, time / _duration);
            _ui.color = _color;

            yield return null;
        }

        _color.a = 0;
        _ui.color = _color;
    }
}
