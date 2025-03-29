using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyDropship
{
    DROPSHIP1,
    DROPSHIP2,
    DROPSHIP3,
    DROPSHIP4,
}

public class EnemyDropShip : MonoBehaviour
{
    [SerializeField] EEnemyDropship eEnemyDropship;

    public EEnemyDropship EEnemyDropship { get { return eEnemyDropship; } }
}
