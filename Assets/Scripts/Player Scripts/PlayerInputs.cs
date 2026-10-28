using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference sprint;

    [SerializeField] private InputActionReference interact;

    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference block;
    [SerializeField] private InputActionReference dodge;
    [SerializeField] private InputActionReference tackle;

    public Vector2 Move => move.action.ReadValue<Vector2>();

    public bool Sprint => sprint.action.IsPressed();
    public bool Interact => interact.action.WasPressedThisFrame();

    public bool Attack => attack.action.WasPressedThisFrame();
    public bool Block => block.action.IsPressed();
    public bool Dodge => dodge.action.WasPressedThisFrame();
    public bool Tackle => tackle.action.WasPressedThisFrame();
}