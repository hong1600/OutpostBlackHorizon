using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] GameObject pressText;
    [SerializeField] GameObject loginText;
    [SerializeField] GameObject namePanel;
    [SerializeField] TMP_InputField nameText;

    bool bAnykey;

    private void Start()
    {
        bAnykey = false;
        Invoke("anykey", 2.5f);
    }

    private void Update()
    {
        checkFirst();
    }

    private void anykey()
    {
        pressText.SetActive(true);
        loginText.SetActive(false);
        bAnykey = true;
    }

    private void checkFirst()
    {
        if (Input.anyKeyDown && bAnykey == true && DataManager.instance.playerdata.first == true)
        {
            namePanel.SetActive(true);
        }
        else if (Input.anyKeyDown && bAnykey == true && DataManager.instance.playerdata.first == false)
        {
            LoadScene.loadScene(1);
        }
    }

    public void checkBtn()
    {
        namePanel.SetActive(false);
        DataManager.instance.playerdata.name = nameText.text;
        DataManager.instance.playerdata.first = false;
        LoadScene.loadScene(1);
        DataManager.instance.saveData();
    }
}
