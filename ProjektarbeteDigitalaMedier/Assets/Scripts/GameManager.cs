using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    //Build Indexes:
    //MainMenu: 0, Shop: 1, InGame: 2

    public static GameManager Instance;
    [HideInInspector]public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    private int startSpeed = 9;
    public static float worldSpeed { get; private set; }// other scripts can read the value but not change it
    private float timer = 0;
    private float timeLimit = 20;
    private float timeLimitIncrement = 5;
    private float speedIncrement = 1;

    public float laneWidth = 3;
    public int numberOfLanes = 3;
    public int startLane = 2;
    [Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private ObstacleSpawner obsSpawner;
    [SerializeField] private Animator fadeAnimator;
    [Header("Menyer")]
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject particlesCrash;
    private int i = 0;


    private int score = 0;
    private float respawnMenuDelay = 2;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;

        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        AudioListener.volume = savedVolume;
    }
    private void Start()
    {
        worldSpeed = startSpeed;
        if (SceneManager.GetActiveScene().buildIndex == 0 && State != GameState.MainMenu)
        {
            ChangeGameState(GameState.MainMenu);
        }
        if (State == GameState.InGame)
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString("d");//one decimal 
        }
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
                PlayerPrefs.SetInt("HasPickedSkin", 0);
                break;
            case GameState.Shop:
                break;
            case GameState.InGame:
                break;
            case GameState.GameOver:
                StartCoroutine(Wait());
                UpdateHighScore();
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

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString("d");//one decimal
    }

    public void IncreaseSpeed()
    {
        worldSpeed += speedIncrement;
        timeLimit += timeLimitIncrement;
        if (i != 10)
        {
            obsSpawner.spawnIntervall -= (speedIncrement / 8);// make obstacles spawn faster
            i += 1;
        }
        timer = 0;
    }

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            Debug.Log("new highscore!!!!!!!!!!!");
        }
    }

    //MainMenu---------------------------------------
    public void PlayGame()
    {
        Time.timeScale = 0;
        StartCoroutine(FadeOut());
        SceneManager.LoadScene(2);
        ChangeGameState(GameState.InGame);
    }
    public void MainMenu()
    {
        StartCoroutine(FadeOut());
        SceneManager.LoadScene(0);
        ChangeGameState(GameState.MainMenu);
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
    public void NewGame()
    {
        Time.timeScale = 0;
        StartCoroutine(FadeOut());
        SceneManager.LoadScene(2);
        ChangeGameState(GameState.InGame);
    }
    public IEnumerator FadeOut()// IEnumerator has access to time related stuff
    {
        fadeAnimator.Play("Fade_Out");
        yield return new WaitForSecondsRealtime(0.4f); //runs even when time.timeScale = 0
    }
    public IEnumerator Wait()// IEnumerator has access to time related stuff
    {
        particlesCrash.SetActive(true);
        worldSpeed = 0;
        obsSpawner.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSecondsRealtime(respawnMenuDelay); //runs even when time.timeScale = 0
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
        pauseButton.SetActive(false);
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
