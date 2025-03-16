using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDefenderAI
{
    CREATE,
    SEARCH,
    ATTACK,
    SKILL,
    RESET,
}

public enum EUnitAnim
{
    IDLE,
    ATTACK,
    SKILL
}

public enum EUnitGrade
{
    NONE,
    C,
    B,
    A,
    S,
    SS
}

public enum EUnitName
{
    SWORDMAN = 101,
    SHIELDMAN = 102,
    KNIGHT = 103,
    GUNSLINGER = 104,
    MAGE = 105,
}