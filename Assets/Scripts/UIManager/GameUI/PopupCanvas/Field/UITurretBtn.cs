using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITurretBtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Image img;

    public void InitTurretBtn(TableTurret.Info _info)
    {
        priceText.text = _info.Cost.ToString();
        img.sprite = SpriteManager.instance.GetSprite(_info.SpriteName);
    }

    public void ClickTurretBtn()
    {

    }
}
