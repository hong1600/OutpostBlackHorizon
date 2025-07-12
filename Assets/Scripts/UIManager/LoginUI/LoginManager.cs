using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputID;
    [SerializeField] TMP_InputField inputPassword;
    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] TextMeshProUGUI statusText;

    [SerializeField] GameObject userNamePanel;
    [SerializeField] TextMeshProUGUI nameStatusText;

    private const string USERKEY = "USER_";
    private const string USERRPASSWORDKEY = "_PASSWORD";

    public void ClickLogin()
    {
        string userID = inputID.text.Trim();
        string userPassword = inputPassword.text.Trim();

        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(userPassword))
        {
            SetstatusText("아이디와 비밀번호를 모두 입력해주세요", Color.red);
            return;
        }

        Login(userID, userPassword);
    }

    private void Login(string _id, string _password)
    {
        string savedID = PlayerPrefs.GetString(USERKEY + _id, "");
        string savedPassword = PlayerPrefs.GetString(USERKEY + _id + USERRPASSWORDKEY, "");

        if (savedID == _id && savedPassword == _password)
        {
            SetstatusText("게임 접속 중...", Color.white);

            UserData loadData = DataManager.instance.UserDataLoader.LoadUserData(_id);

            Debug.Log(DataManager.instance.UserDataLoader.curUserData.userName);

            if (loadData.first)
            {
                userNamePanel.SetActive(true);
            }
            else
            {
                MSceneManager.Instance.ChangeScene(EScene.LOBBY);
            }
        }
        else
        {
            SetstatusText("아이디 또는 비밀번호가 올바르지 않습니다", Color.red);
        }
    }

    public void ClickRegister()
    {
        string userID = inputID.text.Trim();
        string userPassword = inputPassword.text.Trim();

        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(userPassword))
        {
            SetstatusText("아이디와 비밀번호를 모두 입력해주세요", Color.red);
            return;
        }

        if (PlayerPrefs.HasKey(USERKEY + userID))
        {
            SetstatusText("이미 존재하는 아이디입니다. 다른 아이디를 사용해주세요.", Color.red);
            return;
        }

        if (inputID.text.Length < 1) return;

        PlayerPrefs.SetString(USERKEY + userID, userID);
        PlayerPrefs.SetString(USERKEY + userID + USERRPASSWORDKEY, userPassword);
        PlayerPrefs.Save();

        UserData newUserData = new UserData()
        {
            userID = userID,
            userName = "",
            userLevel = 1,
            curExp = 0,
            maxExp = 100,
            gold = 1000,
            gem = 50,
            paper = 10,
            first = true
        };

        DataManager.instance.UserDataLoader.curUserData = newUserData;
        DataManager.instance.UserDataLoader.SaveUserData();
        SetstatusText("회원가입이 완료되었습니다.", Color.white);
    }

    public void ClickRegisterCheck()
    {
        if (!string.IsNullOrEmpty(inputUserName.text) && inputUserName.text.Length > 1)
        {
            DataManager.instance.UserDataLoader.curUserData.userName = inputUserName.text;
            DataManager.instance.UserDataLoader.curUserData.first = false;
            MSceneManager.Instance.ChangeScene(EScene.LOBBY);
            DataManager.instance.UserDataLoader.SaveUserData();
        }
        else
        {
            SetstatusText(nameStatusText.text, Color.red);
        }
    }

    public void ClickRegisterCancle()
    {
        userNamePanel.SetActive(false);
    }

    private void LoadUserData(string _id)
    {
        string userName = PlayerPrefs.GetString(USERKEY + _id, "Unknown");
    }

    private void SetstatusText(string _text, Color color)
    {
        statusText.text = _text;
        statusText.color = color;
    }
}
