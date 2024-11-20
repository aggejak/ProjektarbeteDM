using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    //Build Indexes:
    //MainMenu: 0, Shop: 1, InGame: 2

    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

     public float laneWidth = 3;
     public int numberOfLanes = 3;
     public int startLane = 2;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

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

    public void increaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString("F1");//one decimal
    }

    //MainMenu---------------------------------------
    public void Shop()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
    public void Settings()
    {
        //open settings window
    }
    //-----------------------------------------------
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