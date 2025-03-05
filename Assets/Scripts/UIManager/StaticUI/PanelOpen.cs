using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelOpen : MonoBehaviour
{
    public IEnumerator OpenUI(GameObject _panel, RectTransform[] _rect, Vector2[] _onPos, Vector2[] _offPos)
    {
        for (int i = 0; i < _rect.Length; i++)
        {
            _rect[i].anchoredPosition = _offPos[i];
        }

        yield return null;

        _panel.SetActive(true);

        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < _rect.Length; i++) 
        {
            seq.Join(_rect[i].DOAnchorPos(_onPos[i], 0.3f).SetEase(Ease.OutExpo));
        }
    }

    public IEnumerator CloseUI(GameObject _panel, RectTransform[] _rect, Vector2[] _offPos)
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < _rect.Length; i++)
        {
            seq.Join(_rect[i].DOAnchorPos(_offPos[i], 0.3f).SetEase(Ease.OutExpo));
        }

        yield return new WaitForSeconds(0.2f);

        _panel.SetActive(false);
    }
}
