using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetRightSlot : MonoBehaviour
{
    [SerializeField] Image unitImg;

    public void SetUnit(TableUnit.Info _info)
    {
        unitImg.sprite = SpriteManager.instance.GetSprite(_info.SpriteName);
    }
}
