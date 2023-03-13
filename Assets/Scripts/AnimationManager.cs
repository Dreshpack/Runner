using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public void Run()
    {
        _animator.SetFloat("speed", 1f);
    }

    public void Roll()
    {
        _animator.SetTrigger("slide");
        _animator.SetBool("isJumping", false);
    }

    public void Jump()
    {
        _animator.SetTrigger("jump");
        _animator.SetBool("isJumping", true);
    }

    public void Idle()
    {

    }

    public void Die()
    {
        _animator.SetFloat("speed", 0f);
        _animator.SetBool("isDead", true);
        _animator.SetTrigger("die");
    }

    public void Revive()
    {
        _animator.SetBool("isDead", false);
        _animator.SetFloat("speed", 1f);
    }
}
