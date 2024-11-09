using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UnitRandomSpawnerData", menuName = "UnitManager/UnitRandomSpawnerData", order = 2)]
public class UnitRandomSpawnerData : ScriptableObject
{
    public Image randomUnitImg1;
    public Image randomUnitImg2;
    public Image randomUnitImg3;
    public Sprite[] randomStar;
}
