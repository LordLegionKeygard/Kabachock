using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActions _playerActions;
    private CharacterController _characterController;
    private Animator _animator;
    private PlayerInput _playerInput;
    [SerializeField] private float _moveSpeed;
    private float _rotationSpeed = 600;
    private float _velocityMove;
    private float acceleration = 0.1f;
    private float deceleration = 0.5f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _playerActions = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        if(_playerActions.IsAttack) return;
        Move();
    }

    private void Move()
    {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;

        if (direction.magnitude >= 0.5f)
        {
            Vector3 projectedCameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectedCameraForward, Vector3.up);

            Vector3 moveDirection = rotationToCamera * direction;
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, _rotationSpeed * Time.deltaTime);

            _characterController.Move(moveDirection.normalized * _moveSpeed * Time.deltaTime);

            if (_velocityMove < 1.0f)
            {
                _velocityMove += Time.deltaTime * acceleration;
            }
        }
        else
        {
            if (_velocityMove > 0.0f)
                _velocityMove -= Time.deltaTime * deceleration;

        }
        _animator.SetFloat("moveX", input.x);
        _animator.SetFloat("moveZ", input.y);
        _animator.SetFloat(AnimatorStrings.Speed, _velocityMove);
    }
}
