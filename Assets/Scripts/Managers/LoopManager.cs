using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance { get; private set; }

    [Header("Loop Settings")]
    [SerializeField, Min(1)] private int loopsPerLevel = 3;

    [Header("Level Scenes")]
    [SerializeField] private string[] levelScenes;

    private int currentLoop = 1;
    private int currentLevelIndex;

    public int CurrentLoop => currentLoop;
    public int CurrentLevel => currentLevelIndex + 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        currentLevelIndex = Array.IndexOf(levelScenes, SceneManager.GetActiveScene().name);

        if (currentLevelIndex < 0)
        {
            currentLevelIndex = 0;
        }
    }

    public void CompleteLoop()
    {
        if (levelScenes == null || levelScenes.Length == 0)
        {
            Debug.LogError("No se han configurado escenas de nivel.", this);
            return;
        }

        if (currentLoop < loopsPerLevel)
        {
            currentLoop++;
            ReloadCurrentLevel();
            return;
        }

        currentLoop = 1;
        currentLevelIndex++;

        if (currentLevelIndex >= levelScenes.Length)
        {currentLevelIndex = 0;}

        SceneManager.LoadScene(levelScenes[currentLevelIndex]);
    }

    private void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(levelScenes[currentLevelIndex]);
    }
}