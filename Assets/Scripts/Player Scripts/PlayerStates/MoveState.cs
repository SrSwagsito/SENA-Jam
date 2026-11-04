
public class MoveState : PlayerState
{
    public MoveState(PlayerSTM player) : base(player) { }

    public override void Enter()
    {
        AudioManager.Instance.Play("WalkingFenix");
        player.PlayAnimation("Walk", 0.1f);
    }

    public override void Update()
    {
        if (player.TryStartAction())
            return;

        if (!player.IsGrounded)
        {
            player.ChangeState(player.FallingState);
            return;
        }

        if (inputs.Move.sqrMagnitude <= 0.01f)
        {
            player.ChangeState(player.IdleState);
            return;
        }

        player.ApplyMovement(inputs.Move, inputs.Sprint);
    }
}
