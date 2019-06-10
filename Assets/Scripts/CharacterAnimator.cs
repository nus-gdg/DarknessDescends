using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    protected bool facingDirection = true; //true means spirte is facing right
    protected Animator animator;

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

    public void faceLeft()
    {
        if (facingDirection)
        {
            horizontalDisplayFlip();
            facingDirection = false;
        }
    }

    public void faceRight()
    {
        if (!facingDirection)
        {
            horizontalDisplayFlip();
            facingDirection = true;
        }
    }

    private void horizontalDisplayFlip()
    {
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //Flips the sprite along the x-axis
    }
}
