using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFieldBtn : MonoBehaviour
{
    FieldData fieldData;
    FieldBuild fieldBuild;
    FieldManager fieldManager;
    
    GameObject field;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image img;

    public void InitFieldBtn(FieldData _fieldData)
    {
        fieldManager = FieldManager.instance;
        fieldBuild = fieldManager.FieldBuild;

        fieldManager.FieldBuild.onDecreaseField += UpdateBtn;

        field = _fieldData.field;
        fieldData = _fieldData;
        priceText.text = $"{_fieldData.fieldPrice}";
        amountText.text = $"{fieldManager.GetFieldAmount(_fieldData)}/{_fieldData.fieldAmount}";
        img.sprite = _fieldData.fieldImg;
    }

    private void UpdateBtn()
    {
        amountText.text = $"{fieldManager.GetFieldAmount(fieldData)}/{fieldData.fieldAmount}";
    }

    public void ClickFieldBtn()
    {
        if (fieldManager.GetFieldAmount(fieldData) > 0)
        {
            fieldBuild.BuildPreview(field, fieldData);
        }
    }
}
