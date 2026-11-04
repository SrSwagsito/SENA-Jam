
public class JumpState : PlayerState
{
    public JumpState(PlayerSTM player) : base(player) { }

    public override void Enter()
    {
        //AudioManager.Instance.Play("JumpingFenix");

        player.PlayAnimation("Jump" , 0.05f);
    
        player.StartJump();
    }

    public override void Update()
    {
        player.ApplyMovement(inputs.Move, inputs.Sprint);

        if (player.VerticalVelocity <= 0f)
        {
            player.ChangeState(player.FallingState);
        }
    }
}
