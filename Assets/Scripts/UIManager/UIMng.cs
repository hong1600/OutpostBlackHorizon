using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMng : MonoBehaviour
{
    public static UIMng instance;

    Stack<GameObject> uiStack = new Stack<GameObject>();
    Dictionary<GameObject, UIData> uiDataDic = new Dictionary<GameObject, UIData>();

    [SerializeField] UIOpenPanel uiOpenPanel;

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

    private void Start()
    {
        InputMng.instance.onInputEsc += ClosePanel;
    }

    public void OpenPanel(GameObject _panel, RectTransform[] _rect, Vector2[] _pos, bool _isEffect)
    {
        if (!uiStack.Contains(_panel))
        {
            uiStack.Push(_panel);
            uiDataDic[_panel] = new UIData(_rect, _pos);
        }

        if(_isEffect) 
        {
            StartCoroutine(uiOpenPanel.OpenUI(_panel, _rect, _pos));
        }
        else
        {
            _panel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if(uiStack.Count > 0) 
        {
            GameObject topPanel = uiStack.Pop();

            if (uiDataDic.TryGetValue(topPanel, out UIData uiData))
            {
                uiDataDic.Remove(topPanel);
                StartCoroutine(uiOpenPanel.CloseUI(topPanel, uiData.rects));
            }
            else
            {
                topPanel.SetActive(false);
            }
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
