using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersLoader : MonoBehaviour
{
    [SerializeField] GameObject managers;

    private void Awake()
    {
        if (GameObject.Find("MANAGERS") == null)
        {
            Instantiate(managers);
        }
    }
}
