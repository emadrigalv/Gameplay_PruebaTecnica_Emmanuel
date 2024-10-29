using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Dependencies")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SceneHandler sceneHandler;

    //[Header("Parameters")]
    private int lives;
    private int score;
    private int highScore;
    private int blocksCount;

    private bool isPaused = false;
    private PlayerControls playerControls;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerControls = new PlayerControls();
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        playerControls.Player.Pause.performed += OnPause;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Pause.performed -= OnPause;
        playerControls.Disable();
    }

    public void InitializeGame()
    {
        lives = 5;
        score = 0;
        blocksCount = 50;

        LoadHighScore();

        UpdateHUD(score, highScore, lives);
    }

    public void InitializeNextLevel()
    {
        blocksCount = 50;
        UpdateHUD(score, highScore, lives);
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
        uiManager.UpdateScoreHUD(scoreGained);

        if (score > highScore) 
        {
            highScore = score;
            SaveHighScore();
            uiManager.UpdateHighScoreHUD(highScore);
        }

        if (blocksCount == 0) // ask for level number, level win or game win?
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
        //Stop player movement
        uiManager.LevelWin(score, highScore);
    }

    private void GameWin()
    {
        //Stop player movement
        uiManager.GameWin(score, highScore);
    }

    private void GameOver()
    {
        uiManager.GameOver(score, highScore);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    private void TogglePause()
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
