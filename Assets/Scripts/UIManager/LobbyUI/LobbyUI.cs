using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject blackPanel;
    UserData userData;


    private void Awake()
    {
        if (DataMng.instance.UserDataLoader.curUserData != null)
        {
            userData = DataMng.instance.UserDataLoader.curUserData;
        }
    }

    private void Start()
    {
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
