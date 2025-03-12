using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelectFieldBtn : MonoBehaviour
{
    FieldData fieldData;
    FieldBuild fieldBuild;
    
    GameObject field;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image img;

    private void Start()
    {
        fieldBuild = Shared.fieldManager.FieldBuild;

        Shared.fieldManager.FieldBuild.onDecreaseField += UpdateBtn;
    }

    public void Init(FieldData _fieldData)
    {
        field = _fieldData.field;
        fieldData = _fieldData;
        priceText.text = $"{_fieldData.fieldPrice}";
        amountText.text = $"{Shared.fieldManager.GetFieldAmount(_fieldData)}/{_fieldData.fieldAmount}";
        img.sprite = _fieldData.fieldImg;
    }

    private void UpdateBtn()
    {
        amountText.text = $"{Shared.fieldManager.GetFieldAmount(fieldData)}/{fieldData.fieldAmount}";
    }

    public void ClickFieldBtn()
    {
        if (Shared.fieldManager.GetFieldAmount(fieldData) > 0)
        {
            fieldBuild.BuildPreview(field, fieldData);
        }
    }
}
