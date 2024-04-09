using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayTargetBoolAnimation(bool state, int animation)
    {
        animator.SetBool(animation, state);
    }

    public void AnimatorSetTrigger(int name)
    {
        animator.SetTrigger(name);
    }

    public void AnimatorAttack(int number)
    {
        animator.SetInteger(AnimatorStrings.Attack, number);
        Invoke(nameof(ResetAttackAnimation), 0.1f);
    }

    private void ResetAttackAnimation()
    {
        animator.SetInteger(AnimatorStrings.Attack, 0);
    }

    public void EnableDamageCollider()
    {
        CustomEvents.FireToggleDamageCollider(true);
    }

    public void DisableDamageCollider()
    {
        CustomEvents.FireToggleDamageCollider(false);
    }
}
