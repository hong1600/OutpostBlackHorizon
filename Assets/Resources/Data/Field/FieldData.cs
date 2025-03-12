using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldData", menuName = "FieldData")]
public class FieldData : ScriptableObject
{
    public int fieldID;
    public GameObject field;
    public string fieldName;
    public int fieldAmount;
    public int fieldPrice;
    public Sprite fieldImg;
}
