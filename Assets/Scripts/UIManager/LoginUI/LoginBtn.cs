using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginBtn : MonoBehaviour
{
    [SerializeField] LoginMng loginMng;

    public void ClickLoginBtn()
    {
        loginMng.ClickLogin();
    }

    public void ClickRegisterBtn()
    {
        loginMng.ClickRegister();
    }

    public void ClickRegisterCheckBtn()
    {
        loginMng.ClickRegisterCheck();
    }

    public void ClickRegisterCancleBtn()
    {
        loginMng.ClickRegisterCancle();
    }

    public void ClickDataClearBtn()
    {
        PlayerPrefs.DeleteAll();
    }

}
