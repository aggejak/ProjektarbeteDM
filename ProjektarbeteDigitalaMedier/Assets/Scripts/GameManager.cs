using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
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
        OnGameStateChanged?.Invoke(newState); // has any script subscirbed to the event? if so invoke this
    }
}
public enum GameState
{
    MainMenu,
    Shop,
    InGame,
    GameOver
}