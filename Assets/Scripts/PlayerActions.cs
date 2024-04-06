using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerAnimator _playerAnimator;
    public bool IsJump = false;
    private float _nextJumpTime = 1.8f;
    private bool _isGround;
    private bool _isInAir;
    [SerializeField] private LayerMask groundMask;
    private float _gravity = -20;
    private float _groundDistance = 0.6f;
    private float _airDistance = 1;
    private Vector3 _velocity;
    private float _velocityUpdateY = -5;
    private float _jumpHeight = 1.5f;

    [Header("Attack")]
    [SerializeField] private float _attackTime = 2;
    private bool _isAttack;
    private IEnumerator _attackCoroutine;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        GroundCheck();
        CheckNextJump();
    }

    private void CheckNextJump()
    {
        if (IsJump)
        {
            _nextJumpTime -= Time.deltaTime;
            if (_nextJumpTime < 0)
            {
                IsJump = false;
                _nextJumpTime = 1.8f;
            }
        }
    }

    private void GroundCheck()
    {
        _isGround = Physics.CheckSphere(transform.position, _groundDistance, groundMask);
        _isInAir = Physics.CheckSphere(transform.position, _airDistance, groundMask);
        if (_isGround && _velocity.y < 0) _velocity.y = _velocityUpdateY;
        {
            _velocity.y += _gravity * Time.deltaTime;
            if (_characterController.enabled == true)
                _characterController.Move(_velocity * Time.deltaTime);
        }
        // _playerAnimator.PlayTargetBoolAnimation(!_isInAir, AnimatorStrings.IsInAir);
    }

    public void Jump()
    {
        if (!IsJump)
        {
            _playerAnimator.AnimatorSetTrigger(AnimatorStrings.Jump);
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            IsJump = true;
        }
    }

    public void Attack()
    {
        if (_isAttack) return;

        AttackTimer();
        var rnd = Random.Range(1, 5);
        _playerAnimator.AnimatorAttack(rnd);
    }

    private void AttackTimer()
    {
        _isAttack = true;
        if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
        _attackCoroutine = AttackCoroutine();
        StartCoroutine(_attackCoroutine);
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(_attackTime);
        _isAttack = false;
    }
}
