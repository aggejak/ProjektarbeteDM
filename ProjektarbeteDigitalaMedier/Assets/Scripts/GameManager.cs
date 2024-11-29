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

    public static float worldSpeed { get; private set; } = 9;// other scripts can read the value but not change it
    private float timer = 0;
    private float timeLimit = 20;
    private float timeLimitIncrement = 5;
    private float speedIncrement = 1;

    public float laneWidth = 3;
    public int numberOfLanes = 3;
    public int startLane = 2;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ObstacleSpawner obsSpawner;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;

    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeLimit)
        {
            IncreaseSpeed();
        }
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
                gameOverMenu.SetActive(true);
                Time.timeScale = 0;
                break;
            case GameState.Paused:

                break;
            case GameState.Settings:
                //setActive
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState); // has any script subscirbed to the event? if so invoke this
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString("d");//one decimal
    }

    public void IncreaseSpeed()
    {
        worldSpeed += speedIncrement;
        timeLimit += timeLimitIncrement;
        obsSpawner.spawnIntervall -= (speedIncrement / 3);// make obstacles spawn faster
        timer = 0;
    }

    //MainMenu---------------------------------------
    public void Shop()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
        ChangeGameState(GameState.InGame);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Settings()
    {
        //open settings window
    }
    public void DoExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        ChangeGameState(GameState.Paused);

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        ChangeGameState(GameState.InGame);
        
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