using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    private float _rotationSpeed = 600;
    private float _velocityMove;
    private float acceleration = 0.1f;
    private float deceleration = 0.5f;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
