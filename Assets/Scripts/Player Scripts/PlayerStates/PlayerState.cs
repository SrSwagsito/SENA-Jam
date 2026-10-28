using UnityEngine;

public abstract class PlayerState
{
    protected PlayerSTM player;
    protected PlayerInputs inputs;
    protected Animator animator;
    protected PlayerState(PlayerSTM player)
    {
        this.player = player;
        inputs = player.Inputs;
        //animator = player.Animator;

    }

    public virtual void Enter() { }
    public abstract void Update();
    public virtual void Exit() { }
}
