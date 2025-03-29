using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneType : MonoBehaviour
{
    [SerializeField] EScene eScene;
    public EScene uiSceneType { get { return eScene; } }
}