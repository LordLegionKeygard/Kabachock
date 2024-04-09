using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActions playerActions;
    private CharacterController characterController;
    private Animator animator;
    private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    private float rotationSpeed = 600;
    private float velocityMove;
    private float acceleration = 0.1f;
    private float deceleration = 0.5f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        playerActions = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        if(playerActions.IsAttack) return;
        Move();
    }

    private void Move()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;

        if (direction.magnitude >= 0.5f)
        {
            Vector3 projectedCameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectedCameraForward, Vector3.up);

            Vector3 moveDirection = rotationToCamera * direction;
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, rotationSpeed * Time.deltaTime);

            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            if (velocityMove < 1.0f)
                velocityMove += Time.deltaTime * acceleration;
        }
        else
        {
            if (velocityMove > 0.0f)
                velocityMove -= Time.deltaTime * deceleration;

        }
        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveZ", input.y);
        animator.SetFloat(AnimatorStrings.Speed, velocityMove);
    }
}
