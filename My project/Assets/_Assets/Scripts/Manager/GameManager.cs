using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Dependencies")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SceneHandler sceneHandler;

    [Header("Parameters")]
    [SerializeField] private string sfxLevelWin;
    [SerializeField] private string sfxGameWin;
    [SerializeField] private string sfxGameLose;

    private int lives;
    private int score;
    private int highScore;
    private int blocksCount;
    private int levelIndex;

    private bool isPaused = false;

    private PlayerControls playerControls;
    private PadelController paddle;
    private BallBehaviour ball;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        playerControls = new PlayerControls();
        levelIndex = 0;
    }

    private void OnEnable()
    {
        if (playerControls == null) return;
        playerControls.Player.Pause.performed += OnPause;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        if (playerControls == null) return;
        playerControls.Disable();
        playerControls.Player.Pause.performed -= OnPause;
    }

    public void InitializeGame()
    {
        paddle = GameObject.FindAnyObjectByType<PadelController>();
        ball = GameObject.FindAnyObjectByType<BallBehaviour>();

        lives = 5;
        score = 0;
        blocksCount = 50;

        LoadHighScore();

        UpdateHUD(score, highScore, lives);
    }

    public void InitializeNextLevel()
    {
        paddle = FindAnyObjectByType<PadelController>();
        ball = FindAnyObjectByType<BallBehaviour>();

        blocksCount = 50;
        UpdateHUD(score, highScore, lives);
    }

    public void LoadNewGame(int index)
    {
        levelIndex = index;
        sceneHandler.InitializeGame(levelIndex);
    } 

    public void LoadNextLevel()
    {
        levelIndex++;
        sceneHandler.LoadNextLevel();
    }

    public void RestartLevel()
    {
        sceneHandler.InitializeGame(levelIndex);
    }

    public void MainMenu()
    {
        levelIndex = 0;
        sceneHandler.LoadMainMenu();
    }

    public bool UpdateLives()
    {
        lives--;
        if (lives > 0)
        {
            uiManager.UpdateLivesHUD(lives);
            return true;
        }
        else
        {
            GameOver();
            return false;
        }
    }

    public void UpdateScore(int scoreGained)
    {
        blocksCount--;
        score += scoreGained;
        uiManager.UpdateScoreHUD(score);

        if (score > highScore) 
        {
            highScore = score;
            SaveHighScore();
            uiManager.UpdateHighScoreHUD(highScore);
        }

        if (blocksCount == 0 && levelIndex == 3) // ask for level number, level win or game win?
        {
            GameWin();
        }
        else if (blocksCount == 0 && levelIndex != 3)
        {
            LevelWin();
        }
    }

    public void UpdateHUD(int currentScore, int highScore, int currentLives) 
    {
        uiManager.UpdateScoreHUD(currentScore);
        uiManager.UpdateHighScoreHUD(highScore);
        uiManager.UpdateLivesHUD(currentLives);
    }

    private void LevelWin()
    {
        paddle.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);

        AudioManager.instance.Play(sfxLevelWin);
        uiManager.LevelWin(score, highScore);
    }

    private void GameWin()
    {
        paddle.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);

        AudioManager.instance.Play(sfxGameWin);
        uiManager.GameWin(score, highScore);
    }

    private void GameOver()
    {
        AudioManager.instance.Play(sfxGameLose);
        uiManager.GameOver(score, highScore);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (levelIndex == 0) return;
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        uiManager.Pause(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
