using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnSizeUpEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform btnRect;
    Vector3 originScale;

    private void Start()
    {
        originScale = btnRect.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btnRect.DOScale(originScale * 1.2f, 0.1f).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnRect.DOScale(originScale, 0.1f).SetEase(Ease.OutQuad);
    }
}
