using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerSounds _playerSounds;
    private CharacterController _characterController;
    private PlayerAnimator _playerAnimator;
    public bool _isJump = false;
    private float _currentJumpTime;
    private float _nextTimeJump = 2;
    private bool _isGround;
    [SerializeField] private bool _isInAir;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float _gravity = -10;
    [SerializeField] private float _groundDistance = 0.6f;
    [SerializeField] private float _airDistance = 1;
    private Vector3 _velocity;
    [SerializeField] private float _velocityUpdateY = -1.5f;
    [SerializeField] private float _jumpHeight = 2f;

    [Header("Attack")]
    [SerializeField] private float _attackTime = 2;
    private bool _isAttack;
    public bool IsAttack => _isAttack;
    private IEnumerator _attackCoroutine;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerSounds = GetComponent<PlayerSounds>();
    }

    private void Update()
    {
        GroundCheck();
        CheckNextJump();
    }

    private void CheckNextJump()
    {
        if (_isJump)
        {
            _currentJumpTime -= Time.deltaTime;
            if (_currentJumpTime < 0)
            {
                _isJump = false;
                _currentJumpTime = _nextTimeJump;
            }
        }
    }

    private void GroundCheck()
    {
        _isGround = Physics.CheckSphere(transform.position, _groundDistance, groundMask);
        _isInAir = Physics.CheckSphere(transform.position, _airDistance, groundMask);
        if (_isGround && _velocity.y < 0) _velocity.y = _velocityUpdateY;
        {
            _velocity.y += _gravity/2 * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
        _playerAnimator.PlayTargetBoolAnimation(!_isInAir, AnimatorStrings.IsInAir);
    }

    public void Jump()
    {
        if (!_isJump)
        {
            _playerSounds.JumpSound();
            _playerAnimator.AnimatorSetTrigger(AnimatorStrings.Jump);
            _velocity.y = Mathf.Sqrt(-_jumpHeight * _gravity);
            _isJump = true;
        }
    }

    public void Attack()
    {
        if (_isAttack || _isJump) return;

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
