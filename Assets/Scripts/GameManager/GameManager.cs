using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Timer timer;
    [SerializeField] Round round;
    [SerializeField] GameState gameState;
    [SerializeField] GoldCoin goldCoin;
    [SerializeField] Rewarder rewarder;
    [SerializeField] ViewState viewState;
    [SerializeField] PlayerSpawner playerSpawner;
    [SerializeField] AirStrike airStrike;
    [SerializeField] GuideMissile guideMissile;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    public Timer Timer { get { return timer; }}
    public Round Round { get { return round; } }
    public GameState GameState { get { return gameState; } }
    public GoldCoin GoldCoin { get { return goldCoin; } }
    public Rewarder Rewarder { get { return rewarder; } }
    public ViewState ViewState { get { return viewState; } }
    public PlayerSpawner PlayerSpawner { get { return playerSpawner; } }
    public AirStrike AirStrike { get { return airStrike; } }
    public GuideMissile GuideMissile { get { return guideMissile; } }
}
