using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFieldBtn : MonoBehaviour
{
    Button btn;

    TableField.Info info;
    FieldBuild fieldBuild;
    TurretBuild turretBuild;
    UIBuild uiBuild;

    GameObject prefab;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image img;

    int maxAmount;
    int curAmount;

    public void InitFieldBtn(TableField.Info _info)
    {
        info = _info;
        fieldBuild = FieldManager.instance.FieldBuild;
        turretBuild = FieldManager.instance.TurretBuild;
        uiBuild = GameUI.instance.UIBuild;

        curAmount = info.Amount;
        maxAmount = info.Amount;

        prefab = Resources.Load<GameObject>(info.PrefabPath);
        img.sprite = SpriteManager.instance.GetSprite(info.ImgName);

        costText.text = $"{info.Cost}";
        amountText.text = $"{curAmount}/{maxAmount}";

        btn = GetComponent<Button>();
    }

    public void ClickFieldBtn()
    {
        if (curAmount > 0)
        {
            turretBuild.CancleBuild();
            fieldBuild.CreatePreview(prefab, info.Cost, info.ID);
        }
    }

    public void DecreaseAmount(int amount)
    {
        curAmount = Mathf.Max(0, curAmount - amount);
        amountText.text = $"{curAmount}/{maxAmount}";

        UpdateFieldBtn();
    }

    private void UpdateFieldBtn()
    {
        amountText.text = $"{curAmount}/{maxAmount}";

        if (curAmount <= 0)
        {
            Color color = new Color(0.3f, 0, 0, 0.7f);

            ColorBlock cb = btn.colors;

            cb.normalColor = color;
            cb.highlightedColor = color;
            cb.selectedColor = color;
            cb.pressedColor = color;
            btn.colors = cb;
        }
    }
}
