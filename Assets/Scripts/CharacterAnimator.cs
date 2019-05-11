using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(bool isMoving = true)
    {
        animator.SetBool("Moving", isMoving);
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void Ground(bool isGrounded = true)
    {
        animator.SetBool("Grounded", isGrounded);
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }
}
