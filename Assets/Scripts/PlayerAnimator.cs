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
}
