using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] UiManager UIManager;
    public static GameManager Instance { get; private set; }

    GameState CurrentState;

    public GameState InitialState { get; private set; }

    public static event Action <GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
        AudioManager.Instance.PlayMusic("Music");
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        ExitState(CurrentState);
        CurrentState = newState;
        EnterState(CurrentState);

    }

    public void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                UIManager.ShowScreen("MainMenuHUD");
                Time.timeScale = 1f;
                break;
            case GameState.Gameplay:
                Time.timeScale = 1f;
                UIManager.ShowScreen("GameplayHUD");
                break;
            case GameState.Pause:
                UIManager.ShowOverlay("PauseHUD");
                Time.timeScale = 0f;
                break;
            case GameState.Dialogue:
                UIManager.ShowOverlay("DialogueHUD");
                Time.timeScale = 1f;
                break;
            case GameState.Victory:
                UIManager.ShowOverlay("VictoryHUD");
                Time.timeScale = 0f;
                break;
            case GameState.Defeat:
                UIManager.ShowOverlay("DefeatHUD");
                Time.timeScale = 0f;
                break;


        }
    }

    public void ExitState(GameState state)
    {

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ChangeState(GameState.MainMenu);
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Pause) return;

        ChangeState(GameState.Gameplay);
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Gameplay) return;

        ChangeState(GameState.Pause);
    }


    public void Victory()
    {
        if(CurrentState != GameState.Gameplay && CurrentState != GameState.Pause) return;
        ChangeState(GameState.Victory);
    }

    public void Defeat()
    {
        if (CurrentState != GameState.Gameplay && CurrentState != GameState.Pause) return;
        ChangeState(GameState.Defeat);
    }
    public void RestartLevel()
    {

        ChangeState(GameState.Gameplay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FadeIn()
    {
        UIManager.ShowScreen("BlackoutScreen");
    }
    public void FadeOut()
    {
        UIManager.ShowScreen("BLackIinScreen");
    }


}

