using UnityEngine;
using UnityEngine.UI;

public class BindingButtons : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] Button returnToMenuButton;
    [SerializeField] Button resumeButton;

    GameManager gm;

    private void Awake()
    {
        gm = GameManager.Instance;
    }

    void Start()
    {


        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(gm.PauseGame);
        }

        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.AddListener(gm.ReturnToMainMenu);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(gm.ResumeGame);
        }
    }

    private void OnDestroy()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveListener(gm.PauseGame);
        }

        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.RemoveListener(gm.ReturnToMainMenu);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.RemoveListener(gm.ResumeGame);
        }
    }
}