using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIOpenPanel : MonoBehaviour
{
    [SerializeField] Vector2 offPos;

    public IEnumerator OpenUI(GameObject _panel, RectTransform[] _rect, Vector2[] _onPos)
    {
        for (int i = 0; i < _rect.Length; i++)
        {
            _rect[i].anchoredPosition = offPos;
        }

        yield return null;

        _panel.SetActive(true);

        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < _rect.Length; i++) 
        {
            seq.Join(_rect[i].DOAnchorPos(_onPos[i], 0.3f).SetEase(Ease.OutExpo));
        }
    }

    public IEnumerator CloseUI(GameObject _panel, RectTransform[] _rect)
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < _rect.Length; i++)
        {
            seq.Join(_rect[i].DOAnchorPos(offPos, 0.3f).SetEase(Ease.OutExpo));
        }

        yield return new WaitForSeconds(0.2f);

        _panel.SetActive(false);
    }
}
