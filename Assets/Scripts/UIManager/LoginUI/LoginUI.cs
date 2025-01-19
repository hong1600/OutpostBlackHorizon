using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    [SerializeField] GameObject pressText;
    [SerializeField] GameObject loginText;
    [SerializeField] GameObject namePanel;
    [SerializeField] TMP_InputField nameText;
    [SerializeField] GameObject text1;
    [SerializeField] GameObject text2;
    [SerializeField] GameObject text3;
    [SerializeField] GameObject text4;

    bool isAnykey;

    private void Start()
    {
        isAnykey = false;
        Invoke("Anykey", 2.5f);

        StartAnim();
    }

    private void Update()
    {
        CheckFirst();
    }

    private void Anykey()
    {
        pressText.SetActive(true);
        loginText.SetActive(false);
        isAnykey = true;
    }

    private void CheckFirst()
    {
        if (Input.anyKeyDown && isAnykey == true && DataMng.instance.playerData.first == true)
        {
            namePanel.SetActive(true);
        }
        else if (Input.anyKeyDown && isAnykey == true && DataMng.instance.playerData.first == false)
        {
            Shared.sceneMng.ChangeScene(EScene.LOBBY, true);
        }
    }

    public void CheckBtn()
    {
        namePanel.SetActive(false);
        DataMng.instance.playerData.name = nameText.text;
        DataMng.instance.playerData.first = false;
        Shared.sceneMng.ChangeScene(EScene.LOBBY, true);
    }

    private void StartAnim()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(1);

        Destroy(text1);
        Destroy(text2);
        text4.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        text3.SetActive(true);
    }

}
