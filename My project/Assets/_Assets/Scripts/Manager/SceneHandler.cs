using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;


    public void InitializeGame(int levelIndex)
    {
        StartCoroutine(LoadSceneRoutine(GameManager.instance.InitializeGame, levelIndex));
    }

    public void LoadNextLevel()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadSceneRoutine(GameManager.instance.InitializeNextLevel, levelIndex));    
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneRoutine(null, 0));
    }

    private IEnumerator LoadSceneRoutine(Action function, int sceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!loadOperation.isDone) 
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingBar.value = progress;

            yield return null;
        }

        function?.Invoke();

        loadingScreen.SetActive(false);
    }
}