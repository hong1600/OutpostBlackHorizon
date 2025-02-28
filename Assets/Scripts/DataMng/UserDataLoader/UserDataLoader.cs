using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataLoader : MonoBehaviour
{
    const string USERDATA_KEY = "USERDATA_";

    public void SaveUserData(UserData userData)
    {
        string jsonData = JsonUtility.ToJson(userData, prettyPrint: true);

        PlayerPrefs.SetString(USERDATA_KEY + userData.userID, jsonData);
        PlayerPrefs.Save();
    }

    public UserData LoadUserData(string _id)
    {
        string key = USERDATA_KEY + _id;

        if (PlayerPrefs.HasKey(key))
        {
            string jsonData = PlayerPrefs.GetString(key);
            UserData userData = JsonUtility.FromJson<UserData>(jsonData);

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
