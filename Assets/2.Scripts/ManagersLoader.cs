using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersLoader : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject dataManager;

    private void Awake()
    {
        if (GameObject.Find("DataManager") == null)
        {
            Instantiate(dataManager);
        }
        if (GameObject.Find("GameManager") == null)
        {
            Instantiate(gameManager);
        }
    }
}
