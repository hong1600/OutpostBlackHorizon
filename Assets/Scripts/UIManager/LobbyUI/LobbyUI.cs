using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    UnitDataBase unitData;

    private void Awake()
    {
        unitData = DataMng.instance.UnitDataLoader.unitDataBase;
    }

    private void Start()
    {
        nameText.text = $"{unitData.GetUnitID(101).unitName}";
    }
}
