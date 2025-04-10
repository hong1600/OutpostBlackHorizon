using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITurretBtn : MonoBehaviour
{
    TableTurret.Info info;
    FieldBuild fieldBuild;

    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Image img;

    private void Start()
    {
        fieldBuild = FieldManager.instance.FieldBuild;
    }

    public void InitTurretBtn(TableTurret.Info _info)
    {
        info = _info;
        priceText.text = _info.Cost.ToString();
        img.sprite = SpriteManager.instance.GetSprite(_info.SpriteName);
    }

    public void ClickTurretBtn()
    {
        fieldBuild.BuildPreview(Resources.Load<GameObject>(info.PrefabPath), 0, info.Cost);
    }
}
