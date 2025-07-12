using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public EScene eScene { get; private set; } = EScene.LOBBY;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject blackPanel;

    [SerializeField] GameObject matchingPanel;

    private void Start()
    {
        SetUserName();

        Invoke("OffBlackPanel", 1);
        AudioManager.instance.PlayBgm(EBgm.LOBBYWAITING);

        StartCoroutine(StartLobbyAnim());
    }

    private void SetUserName()
    {
        if (DataManager.instance.UserDataLoader.curUserData != null)
        {
            nameText.text = DataManager.instance.UserDataLoader.curUserData.userName;
        }
        else
        {
            return;
        }
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

    public void ClickQuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
