using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetLeftSlot : MonoBehaviour
{
    [SerializeField] Image unitImg;
    [SerializeField] int num;

    public void SetUnit(TableUnit.Info _unitdata, int _index)
    {
        unitImg.sprite = Resources.Load<Sprite>(_unitdata.ImgPath);
        num = _index;
    }
}
