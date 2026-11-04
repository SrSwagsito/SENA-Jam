
public class FallingState : PlayerState
{
    public FallingState(PlayerSTM player) : base(player) { }

    public override void Enter()
    {
        
        player.PlayAnimation("Falling", 0.01f);
    }

    public override void Update()
    {
        player.ApplyMovement(inputs.Move, inputs.Sprint);

        if (player.IsGrounded && player.VerticalVelocity <= 0f)
        {
            AudioManager.Instance.Play("LandingFenix");
            player.ReturnToMovementState(); 
        }
    }
}
