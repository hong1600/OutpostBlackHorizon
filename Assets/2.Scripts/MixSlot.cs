using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MixSlot : MonoBehaviour
{
    [SerializeField] Image unitImg;
    [SerializeField] TextMeshProUGUI unitNameText;
    GameUI gameUI;
    GameInventory inventory;
    int num;

    private void Start()
    {
        if (gameUI == null)
        {
            gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        }
        if (inventory == null)
        {
            inventory = GameObject.Find("GameInventory").GetComponent<GameInventory>();
        }
    }

    public void setUnit(UnitData unitdata, int index)
    {
        unitImg.sprite = unitdata.unitImg;
        unitNameText.text = unitdata.unitName;
        num = index;
    }

    public void MixBtn()
    {
        gameUI.mixPanel(num);
        inventory.loadRightUnit(num);
        GameManager.Instance.canMixUnit();
    }
}
