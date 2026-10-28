using System;
using UnityEngine;

public class PlayerSTM : MonoBehaviour
{
    public PlayerInputs Inputs { get; private set; }

    //[SerializeField] Animator animator;
    //public Animator Animator => animator;

    public PlayerState CurrentState { get; private set; }

    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] float rotationSpeed = 8f;

    private CharacterController controller;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        IdleState = new IdleState(this);
        MoveState = new MoveState(this);
    }

    private void Start()
    {
        ChangeState(IdleState);
    }

    private void Update()
    {
        CurrentState?.Update();
    }

    public void ChangeState(PlayerState newState)
    {
        if (CurrentState == newState) return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();

        Debug.Log("Entro al estado de " + CurrentState);
    }

    public void MovePlayer(Vector2 input, bool sprint)
    {
        Vector3 direction = transform.right * input.x
                          + transform.forward * input.y;

        direction = Vector3.ClampMagnitude(direction, 1f);

        if (direction.sqrMagnitude > 00.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion smooth = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
            transform.rotation = smooth;
        }

        float speed = sprint
            ? moveSpeed * sprintMultiplier
            : moveSpeed;

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = direction * speed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}