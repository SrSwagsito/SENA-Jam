using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerSTM player) : base(player) { }


    public override void Enter()
    {
    
    }
    public override void Update()
    {
        if (player.Inputs.Move.sqrMagnitude > 0.01f)    
        {
            player.ChangeState(player.MoveState);
            return;
        }

        player.MovePlayer(Vector2.zero, false);
    }
    
}