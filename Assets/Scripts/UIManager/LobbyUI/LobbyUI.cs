using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject blackPanel;
    UnitDataBase unitData;
    UserData userData;


    private void Awake()
    {
        if (DataMng.instance.UserDataLoader.curUserData != null)
        {
            unitData = DataMng.instance.UnitDataLoader.unitDataBase;
            userData = DataMng.instance.UserDataLoader.curUserData;
        }
    }

    private void Start()
    {
        if (DataMng.instance.UserDataLoader.curUserData != null)
        {
            nameText.text = $"{unitData.GetUnitID(101).unitName}{userData.userName}";
        }

        Invoke("OffBlackPanel", 1);

        StartCoroutine(StartLobbyAnim());
    }

    private void OffBlackPanel()
    {
        blackPanel.SetActive(false);
    }

    IEnumerator StartLobbyAnim()
    {
        yield return new WaitForSeconds(1);
    }
}
