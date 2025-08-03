using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameMode
{
    SINGLE,
    MULTI
}

public class GameModeManager : Singleton<GameModeManager>
{
    public EGameMode eGameMode { get; private set; }

    public void ChangeGameMode(EGameMode _eGameMode)
    {
        switch (_eGameMode)
        {
            case EGameMode.SINGLE:
                eGameMode = EGameMode.SINGLE; 
                break;
            case EGameMode.MULTI:
                eGameMode = EGameMode.MULTI; 
                break;
        }
    }
}
