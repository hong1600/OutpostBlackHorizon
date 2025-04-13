using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITurretBtn : MonoBehaviour
{
    TableTurret.Info info;
    TurretBuild turretBuild;
    FieldBuild fieldBuild;

    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Image img;

    private void Start()
    {
        turretBuild = FieldManager.instance.TurretBuild;
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
        fieldBuild.CancleBuild();
        turretBuild.CreatePreview(Resources.Load<GameObject>(info.PrefabPath), 0, info.Cost);
    }
}
