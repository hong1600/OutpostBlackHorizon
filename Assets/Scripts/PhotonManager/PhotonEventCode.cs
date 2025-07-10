using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhotonEventCode
{
    public const byte MATCHING_GAME_EVENT = 1;
    public const byte LOAD_COMPLETE_EVENT = 2;
    public const byte START_GAME_EVENT = 3;
    public const byte ARRIVE_DROPSHIP_EVENT = 4;
    public const byte SPAWN_PLAYER_EVENT = 5;
    public const byte GAME_CHAT_EVENT = 6;
    public const byte ENEMY_SPAWN_EVENT = 7;
    public const byte ENEMY_SYNC_EVENT = 8;
    public const byte TIMER_UPDATE_EVENT = 9;
    public const byte REST_TIME_EVENT = 10;
    public const byte SPAWN_TIME_EVENT = 11;
    public const byte ENEMY_STATE_EVENT = 12;
    public const byte SPAWN_BULLET_EVENT = 13;
    public const byte SPAWN_GRENADE_EVENT = 13;
}
