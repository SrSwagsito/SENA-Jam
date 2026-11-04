using UnityEngine;
public class IdleState : PlayerState
{
    public IdleState(PlayerSTM player) : base(player) { }


    public override void Enter()
    {
        player.PlayAnimation("Idle" , 0.05f);
    }
    public override void Update()
    {
        player.ApplyMovement(Vector2.zero, false);

        if (player.TryStartAction())
            return;

        if (!player.IsGrounded)
        {
            player.ChangeState(player.FallingState);
            return;
        }

        if (inputs.Move.sqrMagnitude > 0.01f)
        { 
            player.ChangeState(player.MoveState); 
        }
    }
}
