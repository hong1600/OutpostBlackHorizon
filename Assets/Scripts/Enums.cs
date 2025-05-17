using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyAI
{
    CREATE,
    MOVE,
    ATTACK,
    STAY,
    DIE,
    RESET,
}

public enum EEnemyAnim
{
    IDLE,
    WALK,
    ATTACK,
    DIE,
}

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
    C,
    B,
    A,
    S,
    SS
}

public enum EPlayer
{
    MOVE,
    DIE,
}

public enum EPlayerAnim
{
    MOVE = 0,
    DIE = 1,
}


public enum ESfxObj
{
    SFX,
}

public enum EScene
{
    LOGIN,
    LOBBY,
    WAITING,
    GAME,
    LOADING,
    END,
}

public enum EEffect
{
    SWORD,
    SHIELD,
    KNIGHT,
    GUNSLINGER,
    MAGE,
    GUNSPARK,
    PLASMA,
    ENEMYEXPLOSION,
    ROCKETEXPLOSION,
    DUSTEXPLOSION,
    GRENADETURRETEXPLOSION
}

public enum EHpBar
{
    NORMAL,
    WAVEBOSS,
    BOSS
}

public enum EEnemy
{
    ROBOT1,
    ROBOT2,
    ROBOT3,
    ROBOT4,
    ROBOT5,
    ROBOT6,
}

public enum EBullet
{
    PLAYERBULLET,
    PLAYERGRENADE,
    MACHINEBULLET,
    ROCKETMISSILE,
    LASERBULLET,
    ROBOT2BULLET,
    BOSSMISSILE,
    TURRETGRENADE,
    AMMOPACK
}

public enum EBgm
{
    LOGIN,
    LOBBYWAITING,
    GAMESTART,
    GAME,
    WATER,
}

public enum ESfx
{
    FOOTSTEP,
    GUNSHOT,
    GUNRELOAD,
    GRENADESHOT,
    EXPLOSION,
    CLICK,
    LASER,
    MACHINEGUN,
    GRENADETURRET
}

public enum EGameResource
{
    ENEMY1, ENEMY2, ENEMY3, ENEMY4, ENEMY5, ENEMY6
}

public enum EObject
{
    BULLET,
    TURRET,
}