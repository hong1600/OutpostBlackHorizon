using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetLeftSlot : MonoBehaviour
{
    [SerializeField] Image unitImg;
    [SerializeField] int num;

    public void SetUnit(TableUnit.Info _info, int _index)
    {
        unitImg.sprite = SpriteManager.instance.GetSprite(_info.SpriteName);
        num = _index;
    }
}
