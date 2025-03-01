using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    UnitDataBase unitData;
    UserData userData;


    private void Awake()
    {
        unitData = DataMng.instance.UnitDataLoader.unitDataBase;
        userData = DataMng.instance.UserDataLoader.curUserData;
    }

    private void Start()
    {
        nameText.text = $"{unitData.GetUnitID(101).unitName}{userData.userName}";
    }
}
