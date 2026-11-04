using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerSTM : MonoBehaviour
{

    [SerializeField] Animator animator;
    private string currentAnimation = "Idle";

    public PlayerInputs Inputs { get; private set; }
    public PlayerState CurrentState { get; private set; }

    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public PaintingState PaintingState { get; private set; }
    public AttackState AttackState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallingState FallingState { get; private set; }

    public Interactuable CurrentInteractable { get; private set; }
    private List<Interactuable> interactablesInRange = new List<Interactuable>();

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float gravity = -9f;
    [SerializeField] float rotationSpeed = 8f;
    [SerializeField] float jumpHeight = 1.5f;

    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius = 2f;
    [SerializeField] private LayerMask interactionLayers = ~0;


    private CharacterController controller;
    public float VerticalVelocity => verticalVelocity;
    public float verticalVelocity;
    public bool IsGrounded => controller.isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Inputs = GetComponent<PlayerInputs>();

        IdleState = new IdleState(this);
        MoveState = new MoveState(this);
        PaintingState = new PaintingState(this);
        AttackState = new AttackState(this);
        JumpState = new JumpState(this);
        FallingState = new FallingState(this);
    }

    private void Start()
    {
        ChangeState(IsGrounded ? IdleState : FallingState);
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

    public bool TryStartAction()
    {
        if (!IsGrounded)
            return false;

        if (Inputs.Jump)
        {
            ChangeState(JumpState);
            return true;
        }

        if (Inputs.Attack)
        {
            ChangeState(AttackState);
            return true;
        }

        if (Inputs.Interact && TryFindInteractable())
        {
            ChangeState(PaintingState);
            return true;
        }

        return false;
    }

    public void StartJump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }


    private bool TryFindInteractable()
    {
        Vector3 origin = interactionPoint != null ? interactionPoint.position : transform.position + Vector3.up;

        Collider[] hits = Physics.OverlapSphere(origin, interactionRadius, interactionLayers, QueryTriggerInteraction.Collide);

        Interactuable nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            Interactuable candidate = hit.GetComponentInParent<Interactuable>();

            if (candidate == null || !candidate.CanInteract)
                continue;

            float distance = (candidate.transform.position - origin).sqrMagnitude;

            if (distance < nearestDistance)
            {
                nearest = candidate;
                nearestDistance = distance;
            }
        }

        CurrentInteractable = nearest;
        return nearest != null;
    }

    public void ReturnToMovementState()
    {
        CurrentInteractable = null;

        ChangeState(Inputs.Move.sqrMagnitude > 0.01f ? MoveState : IdleState);
    }


    public void ApplyMovement(Vector2 input, bool sprint)
    {
        Vector3 direction = transform.right * input.x + transform.forward * input.y;

        direction = Vector3.ClampMagnitude(direction, 1f);

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        float speed = sprint ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 velocity = direction * speed;

        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    public void PlayAnimation(string animationName, float fadeTime)
    {
        if (animator == null) return;

        if(currentAnimation == null) { Debug.Log("No se encontro la animacion de " + currentAnimation); return; }

        currentAnimation = animationName;
        
        animator.CrossFadeInFixedTime(animationName, fadeTime);

    }
}