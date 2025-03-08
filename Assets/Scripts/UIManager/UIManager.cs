using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    Stack<GameObject> uiStack = new Stack<GameObject>();
    Dictionary<GameObject, UIData> uiDataDic = new Dictionary<GameObject, UIData>();

    [SerializeField] PanelOpen panelOpen;
    [SerializeField] VideoSelector videoSelector;

    public PanelOpen PanelOpen { get; private set; }
    public VideoSelector VideoSelector { get; private set; }

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

        PanelOpen = panelOpen;
        VideoSelector = videoSelector;
    }

    private void Start()
    {
        InputManager.instance.onInputEsc += ClosePanel;
    }

    public void OpenPanel(GameObject _panel, RectTransform[] _rect, 
        Vector2[] _onPos, Vector2[] _offPos, bool _isEffect)
    {
        //if (uiStack.Count > 0)
        //{
        //    GameObject lastPanel = uiStack.Peek();
        //    CanvasGroup lastCanvasGroup = lastPanel.GetComponent<CanvasGroup>();

        //    if (lastCanvasGroup == null)
        //    {
        //        lastPanel.AddComponent<CanvasGroup>();
        //        lastCanvasGroup = lastPanel.GetComponent<CanvasGroup>();
        //    }

        //    lastCanvasGroup.blocksRaycasts = false;
        //}
        if (!uiStack.Contains(_panel))
        {
            uiStack.Push(_panel);
            uiDataDic[_panel] = new UIData(_rect, _onPos, _offPos);
        }

        if (_isEffect) 
        {
            StartCoroutine(panelOpen.OpenUI(_panel, _rect, _onPos, _offPos));
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
                StartCoroutine(panelOpen.CloseUI(topPanel, uiData.rects, uiData.offPos));
            }
            else
            {
                topPanel.SetActive(false);
            }

            //if (uiStack.Count > 0)
            //{
            //    GameObject newTopPanel = uiStack.Peek();
            //    CanvasGroup newTopCanvasGroup = newTopPanel.GetComponent<CanvasGroup>();

            //    if (newTopCanvasGroup != null)
            //    {
            //        newTopCanvasGroup.blocksRaycasts = true;
            //    }
            //}
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
