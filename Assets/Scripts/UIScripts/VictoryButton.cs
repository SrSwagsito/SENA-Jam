using Unity.VisualScripting;
using UnityEngine;

public class VictoryButton : MonoBehaviour
{
    public void ContinueNextLoop()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);

        if (LoopManager.Instance != null)
        {
            AudioManager.Instance.Play("UIClic");
            LoopManager.Instance.CompleteLoop();
        }
        else 
        {
            Debug.LogWarning("No existe el LoopManager"); 
        }
    }
}
