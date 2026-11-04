
using UnityEngine;

public class PaintingState : PlayerState
{
    private float timer;
    private const float PaintDuration = 0.8f;

    public PaintingState(PlayerSTM player) : base(player) { }

    public override void Enter()
    {
        timer = PaintDuration;

        if (player.CurrentInteractable != null)
        {
            AudioManager.Instance.Play("GruntFenix");
            AudioManager.Instance.Play("Painting");
            AudioManager.Instance.Play("Brushing");
            player.PlayAnimation("Painting", 0.05f);
        }
            
    }

    public override void Update()
    {
        player.ApplyMovement(Vector2.zero, false);
        timer -= Time.deltaTime;

        if (!inputs.Interact)
        {
            player.ReturnToMovementState();
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (player.CurrentInteractable != null)
            {
                player.CurrentInteractable.Interact();
                player.ReturnToMovementState();
            }
           
        }
    }
}
