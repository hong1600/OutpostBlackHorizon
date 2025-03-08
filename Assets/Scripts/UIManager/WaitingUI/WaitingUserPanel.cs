using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingUserPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expText;

    private void Start()
    {
        if (DataManager.instance.UserDataLoader.curUserData != null)
        {
            nameText.text = $"{DataManager.instance.UserDataLoader.curUserData.userName}";
            levelText.text = $"LV.{DataManager.instance.UserDataLoader.curUserData.userLevel}";
            expText.text = $"XP: {DataManager.instance.UserDataLoader.curUserData.curExp}/" +
                $"{DataManager.instance.UserDataLoader.curUserData.maxExp}";
        }
    }
}
