
using UnityEngine;

public class AttackState : PlayerState
{
    private float timer;
    private const float AttackDuration = 0.5f;

    public AttackState(PlayerSTM player) : base(player) { }

    public override void Enter()
    {
        AudioManager.Instance.Play("SlashingFenix");
        player.PlayAnimation("Attack", 0.1f);
        timer = AttackDuration;
    }

    public override void Update()
    {
        player.ApplyMovement(Vector2.zero, false);
        timer -= Time.deltaTime;

        if (timer <= 0f)
            player.ReturnToMovementState();
    }
}
