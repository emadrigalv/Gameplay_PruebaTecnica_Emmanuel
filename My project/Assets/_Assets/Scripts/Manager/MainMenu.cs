using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadGameLevel(int level)
    {
        GameManager.instance.LoadNewGame(level);
    }

    public void PlayMenuSound(string sound)
    {
        AudioManager.instance.Play(sound);
    }

    public void CloseApp()
    {
        Application.Quit();

        Debug.Log("App Closed");
    }
}
