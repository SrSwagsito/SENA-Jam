using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30;
    [SerializeField] private TMP_Text timerText;

    private float timeRemaining;
    private bool timeIsUp;

    private void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerText();
    }

    private void Update()
    {
        if (timeIsUp)
            return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0f, timeRemaining);

        UpdateTimerText();

        if (timeRemaining <= 0f)
        {
            timeIsUp = true;

            if (GameManager.Instance != null)
            { GameManager.Instance.Defeat(); }
            else
               { Debug.LogError("No existe un GameManager en la escena."); }
        }
    }

    private void UpdateTimerText()
    {
        if (timerText == null)
            return;

        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = seconds.ToString();
    }
}