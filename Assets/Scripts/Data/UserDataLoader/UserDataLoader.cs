using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataLoader : MonoBehaviour
{
    public UserData curUserData;
    const string USERDATA_KEY = "USERDATA_";

    public void SaveUserData()
    {
        string jsonData = JsonUtility.ToJson(curUserData, prettyPrint: true);

        PlayerPrefs.SetString(USERDATA_KEY + curUserData.userID, jsonData);
        PlayerPrefs.Save();
    }

    public UserData LoadUserData(string _id)
    {
        string key = USERDATA_KEY + _id;

        if (PlayerPrefs.HasKey(key))
        {
            string jsonData = PlayerPrefs.GetString(key);
            UserData userData = JsonUtility.FromJson<UserData>(jsonData);
            curUserData = userData;

            return userData;
        }
        else
        {
            return null;
        }
    }

    public void ClearUserData(string _id)
    {
        string key = USERDATA_KEY + _id;
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }
}
