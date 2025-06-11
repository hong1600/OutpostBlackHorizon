using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameChat : MonoBehaviour
{
    [SerializeField] GameObject messagePrefab;
    [SerializeField] GameObject inputfieldObj;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Transform messageParent;

    [SerializeField] Image chatPanelImg;
    Color openColor;
    Color closeColor;

    Queue<GameObject> messageQueue = new Queue<GameObject>();
    int maxMessage = 6;

    private void Awake()
    {
        closeColor = chatPanelImg.color;
        openColor = new Color(openColor.r, openColor.g, openColor.b, 0.5f);
    }

    private void Update()
    {
        if (!inputfieldObj.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            ChangeColor(openColor);
            inputfieldObj.SetActive(true);
            inputField.ActivateInputField();
            inputField.Select();
            InputManager.instance.LockInput();
        }
        else if (inputfieldObj.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrWhiteSpace(inputField.text))
            {
                SendMessage();
            }

            ChangeColor(closeColor);
            inputfieldObj.SetActive(false);
            InputManager.instance.UnlockInput();
        }
    }

    public void SendMessage()
    {
        if (string.IsNullOrEmpty(inputField.text)) return;

        string sender = "";

        if (DataManager.instance.UserDataLoader.curUserData != null)
        {
            sender = DataManager.instance.UserDataLoader.curUserData.userName;
        }
        else
        {
            sender = "Test";
        }

        string message = inputField.text;

        if (GameModeManager.instance.eGameMode == EGameMode.MULTI)
        {
            PhotonManager.instance.PhotonChat.SendChat(sender, message);
        }
        else
        {
            DisplayMessage(sender, message);
        }

        inputField.text = "";
        inputField.ActivateInputField();
    }

    public void DisplayMessage(string _sender, string _message)
    {
        GameObject msgObj = Instantiate(messagePrefab, messageParent);
        TMP_Text text = msgObj.GetComponent<TMP_Text>();
        text.text = $"{_sender}: {_message}";

        messageQueue.Enqueue(msgObj);
        LimitMessageCount();
    }

    private void ChangeColor(Color _color)
    {
        chatPanelImg.color = _color;
    }

    private void LimitMessageCount()
    {
        if(messageQueue.Count > maxMessage) 
        {
            Destroy(messageQueue.Dequeue());
        }
    }
}
