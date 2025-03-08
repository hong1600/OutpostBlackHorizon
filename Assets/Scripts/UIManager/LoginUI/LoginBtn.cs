using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginBtn : MonoBehaviour
{
    [SerializeField] LoginManager loginManager;

    public void ClickLoginBtn()
    {
        loginManager.ClickLogin();
    }

    public void ClickRegisterBtn()
    {
        loginManager.ClickRegister();
    }

    public void ClickRegisterCheckBtn()
    {
        loginManager.ClickRegisterCheck();
    }

    public void ClickRegisterCancleBtn()
    {
        loginManager.ClickRegisterCancle();
    }

    public void ClickDataClearBtn()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("DATA CLEAR!");
    }

}
