using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void PlayTargetBoolAnimation(bool state, int animation)
    {
        _animator.SetBool(animation, state);
    }

    public void AnimatorSetTrigger(int name)
    {
        _animator.SetTrigger(name);
    }

    public void AnimatorAttack(int number)
    {
        _animator.SetInteger(AnimatorStrings.Attack, number);
        Invoke(nameof(ResetAttackAnimation), 0.1f);
    }

    private void ResetAttackAnimation()
    {
        _animator.SetInteger(AnimatorStrings.Attack, 0);
    }

    public void EnableDamageCollider()
    {
        CustomEvents.FireToggleDamageCollider(true);
        //Attack Sound
    }

    public void DisableDamageCollider()
    {
        CustomEvents.FireToggleDamageCollider(false);
    }


}
