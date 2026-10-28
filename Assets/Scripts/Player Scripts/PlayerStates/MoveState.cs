public class MoveState : PlayerState
{
    public MoveState(PlayerSTM player) : base(player) { }

    public override void Enter()
    {
        
    }
    public override void Update()
    {
        if (inputs.Move.sqrMagnitude <= 0.01f)
        {
            player.ChangeState(player.IdleState);
            return;
        }

        player.MovePlayer(inputs.Move, inputs.Sprint);
    }

    
}