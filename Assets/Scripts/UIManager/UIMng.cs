using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
