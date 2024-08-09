using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersLoader : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject BtnManager;
    [SerializeField] GameObject UIManager;

    private void Awake()
    {
        if (GameObject.Find("GameManager") == null)
        {
            Instantiate(gameManager);
        }
        if (GameObject.Find("BtnManager") == null)
        {
            Instantiate(gameManager);
        }
        if (GameObject.Find("UIManager") == null)
        {
            Instantiate(gameManager);
        }

    }
}
