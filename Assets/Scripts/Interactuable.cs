using UnityEngine;

public abstract class Interactuable : MonoBehaviour, IInteractuable
{
    public virtual bool CanInteract => true;

    public abstract void Interact();
    

}
