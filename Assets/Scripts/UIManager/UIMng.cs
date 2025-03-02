using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMng : MonoBehaviour
{
    public static UIMng instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void OpenPanel(GameObject _panel)
    {
        if (_panel.activeSelf == false)
        {
            _panel.SetActive(true);
            _panel.transform.localScale = Vector3.zero;
            _panel.transform.DOScale(Vector3.one, 0.2f);
        }
    }

    public void ClosePanel(GameObject _button)
    {
        Transform buttonParent = _button.transform.parent;

        if (buttonParent != null)
        {
            buttonParent.gameObject.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                buttonParent.gameObject.SetActive(false);
            });
        }
    }

    public IEnumerator StartFadeOut(Graphic _ui, float _duration)
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

    public IEnumerator StartFadeIn(Graphic _ui, float _duration)
    {
        Color _color = _ui.color;
        float startAlpha = _color.a;
        float time = 0f;

        while (time < _duration)
        {
            time += Time.deltaTime;
            _color.a = Mathf.Lerp(startAlpha, time / _duration, 100);
            _ui.color = _color;

            yield return null;
        }

        _color.a = 100;
        _ui.color = _color;
    }


    public void SetAlpha(Graphic _ui, float _alpha)
    {
        Color color = _ui.color;
        color.a = _alpha;
        _ui.color = color;
    }
}
