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

    [SerializeField] GameObject unText;
    [SerializeField] GameObject bbalText;
    [SerializeField] GameObject unbbalText;
    [SerializeField] GameObject jonmangameText;

    bool bAnykey;

    private void Start()
    {
        DataManager.instance.loadData();

        bAnykey = false;
        Invoke("anykey", 2.5f);

        startAnim();
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
    }

    private void startAnim()
    {
        StartCoroutine(startanim());
    }

    IEnumerator startanim()
    {
        yield return new WaitForSeconds(1);

        Destroy(unText);
        Destroy(bbalText);
        jonmangameText.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        unbbalText.SetActive(true);
    }

}
