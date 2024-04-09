using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerSounds playerSounds;
    private CharacterController characterController;
    private PlayerAnimator playerAnimator;

    [Header("Jump")]
    [SerializeField] private LayerMask groundMask;
    private bool isInAir;
    private Vector3 velocity;
    private float velocityUpdateY = -1.5f;
    private float airDistance = 0.3f;
    private float groundDistance = 0.3f;
    private float gravity = -15;
    private float currentJumpTime;
    private float nextTimeJump = 2;
    private float jumpHeight = 1;
    public bool isJump = false;
    private bool isGround;

    [Header("Attack")]
    private float attackTime = 1.3f;
    private bool isAttack;
    public bool IsAttack => isAttack;
    private IEnumerator attackCoroutine;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerSounds = GetComponent<PlayerSounds>();
    }

    private void Update()
    {
        GroundCheck();
        CheckNextJump();
    }

    private void CheckNextJump()
    {
        if (isJump)
        {
            currentJumpTime -= Time.deltaTime;
            if (currentJumpTime < 0)
            {
                isJump = false;
                currentJumpTime = nextTimeJump;
            }
        }
    }

    private void GroundCheck()
    {
        isGround = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        isInAir = Physics.CheckSphere(transform.position, airDistance, groundMask);
        if (isGround && velocity.y < 0) velocity.y = velocityUpdateY;
        {
            velocity.y += gravity/2 * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        playerAnimator.PlayTargetBoolAnimation(!isInAir, AnimatorStrings.IsInAir);
    }

    public void Jump()
    {
        if (!isJump)
        {
            playerSounds.JumpSound();
            playerAnimator.AnimatorSetTrigger(AnimatorStrings.Jump);
            velocity.y = Mathf.Sqrt(-jumpHeight * gravity);
            isJump = true;
        }
    }

    public void Attack()
    {
        if (isAttack || isJump) return;

        AttackTimer();
        var rnd = Random.Range(1, 5);
        playerAnimator.AnimatorAttack(rnd);
    }

    private void AttackTimer()
    {
        isAttack = true;
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = AttackCoroutine();
        StartCoroutine(attackCoroutine);
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
    }
}
