using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    //Build Indexes:
    //MainMenu: 0, Shop: 1, InGame: 2

    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }
    public void ChangeGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Shop:
                break;
            case GameState.InGame:
                break;
            case GameState.GameOver:
                break;
            case GameState.Paused:
                break;
            case GameState.Settings:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState); // has any script subscirbed to the event? if so invoke this
    }
}
public enum GameState
{
    MainMenu,
    Shop,
    InGame,
    GameOver,
    Paused,
    Settings
}