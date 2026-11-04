using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private int tasksPerLoop = 5;
    private readonly List<PaintableTask> selectedTasks = new();
    private int completedTasks;
    bool loopCompleted;
    private void Start()
    {
        PaintableTask[] allTasks = FindObjectsByType<PaintableTask>(FindObjectsSortMode.None);

        List<PaintableTask> availableTasks = new List<PaintableTask>(allTasks);

        foreach (PaintableTask task in availableTasks)
        {
            task.SetSelected(false);
        }

        int amount = Mathf.Min(tasksPerLoop, availableTasks.Count);


        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(i, availableTasks.Count);

            (availableTasks[i], availableTasks[randomIndex]) = (availableTasks[randomIndex], availableTasks[i]);

            PaintableTask task = availableTasks[i];

            task.SetSelected(true);
            task.OnCompleted += HandleTaskCompleted;
            selectedTasks.Add(task);
        }

        if (amount == 0)
        {
            Debug.LogWarning("No hay pintables.");
        }
    }

    private void HandleTaskCompleted(PaintableTask task)
    {
        task.OnCompleted -= HandleTaskCompleted;

        completedTasks++;

        if (completedTasks >= selectedTasks.Count && !loopCompleted)
        {
            loopCompleted = true;

            if (loopCompleted)
            {
                GameManager.Instance.ChangeState(GameState.Victory);
            }
            else { Debug.LogWarning("No existe el gameManager"); }
        }
    }

    private void OnDestroy()
    {
        foreach (PaintableTask task in selectedTasks)
        {
            if (task != null)
            { 
                task.OnCompleted -= HandleTaskCompleted; 
            }
        }
    }
}