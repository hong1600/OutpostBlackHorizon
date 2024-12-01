using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptble Object/EnemyData")]

public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float enemySpeed;
    public float enemyHp;
}
