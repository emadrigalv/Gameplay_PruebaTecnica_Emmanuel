using TMPro;
using UnityEngine;

internal class UIManager
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseMenu;

    [Header("HUD")]
    [SerializeField] private GameObject hudMenu;
    [SerializeField] private TMP_Text hudScoreText;
    [SerializeField] private TMP_Text hudHighScoreText;
    [SerializeField] private TMP_Text hudLivesText;

    [Header("Level Win")]
    [SerializeField] private GameObject levelWinMenu;
    [SerializeField] private TMP_Text lwScoreText;
    [SerializeField] private TMP_Text lwHighScoreText;

    [Header("Game Win")]
    [SerializeField] private GameObject gameWinMenu;
    [SerializeField] private TMP_Text gwScoreText;
    [SerializeField] private TMP_Text gwHighScoreText;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TMP_Text goScoreText;
    [SerializeField] private TMP_Text goHighScoreText;

    public void UpdateScoreHUD(int score)
    {
        hudScoreText.text = score.ToString();
    }

    public void UpdateLivesHUD(int lives)
    {
        hudLivesText.text = lives.ToString();
    }

    public void UpdateHighScoreHUD(int highScore)
    {
        hudHighScoreText.text = highScore.ToString();
    }

    public void Pause(bool isPause)
    {
        pauseMenu.SetActive(isPause);
    }

    public void LevelWin(int score, int highScore)
    {
        levelWinMenu.SetActive(true);
        FillText(lwScoreText, lwHighScoreText, score, highScore);
    }

    public void GameWin(int score, int highScore)
    {
        gameWinMenu.SetActive(true);
        FillText(gwScoreText, gwHighScoreText, score, highScore);
    }

    public void GameOver(int score, int highScore)
    {
        gameOverMenu.SetActive(true);
        FillText(goScoreText, goHighScoreText, score, highScore);
    }

    private void FillText(TMP_Text scoreText, TMP_Text highScoreText, int score, int highScore)
    {
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
    }
}