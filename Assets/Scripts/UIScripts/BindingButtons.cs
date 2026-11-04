
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BindingButtons : MonoBehaviour
{
    [SerializeField] private List<Button> playButtons = new();
    [SerializeField] private List<Button> resumeButtons = new();
    [SerializeField] private List<Button> restartButtons = new();
    [SerializeField] private List<Button> pauseButtons = new();
    [SerializeField] private List<Button> returnMenuButtons = new();

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;

        RegisterButtons();
    }

    private void RegisterButtons()
    {
        Register(playButtons, gm.StartGame);

        Register(resumeButtons, gm.ResumeGame);

        Register(restartButtons, gm.RestartLevel);

        Register(pauseButtons, gm.PauseGame);

        Register(returnMenuButtons, gm.ReturnToMainMenu);

    }

    private void Register(List<Button> buttons, UnityEngine.Events.UnityAction action)
    {
        foreach (Button button in buttons)
        {
            if (button != null)
            {

                button.onClick.AddListener(action);
            }
        }
    }

    private void OnDestroy()
    {
        if (gm == null)
            return;

        Unregister(playButtons, gm.StartGame);

        Unregister(resumeButtons, gm.ResumeGame);

        Unregister(restartButtons, gm.RestartLevel);

        Unregister(pauseButtons, gm.PauseGame);

        Unregister(returnMenuButtons, gm.ReturnToMainMenu);
    }

    private void Unregister(List<Button> buttons, UnityEngine.Events.UnityAction action)
    {
        foreach (Button button in buttons)
        {
            if (button != null)
            {
                button.onClick.RemoveListener(action);
            }
        }
    }
}