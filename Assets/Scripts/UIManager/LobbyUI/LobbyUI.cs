using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public EScene eScene { get; private set; } = EScene.LOBBY;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject blackPanel;
    UserData userData;

    [SerializeField] GameObject matchingPanel;

    private void Start()
    {
        if (DataManager.instance.UserDataLoader.curUserData != null)
        {
            userData = DataManager.instance.UserDataLoader.curUserData;
        }

        Invoke("OffBlackPanel", 1);
        AudioManager.instance.PlayBgm(EBgm.LOBBYWAITING);

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

    public void ClickSingleBtn()
    {
        MSceneManager.Instance.ChangeScene(EScene.WAITING);
    }

    public void ClickMachingBtn()
    {
        matchingPanel.SetActive(true);
    }
}
