using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(MovementController))]
public class PlayerMovement : MonoBehaviour
{
    private MovementController movementController;
    private CharacterAnimator charAnimator;

    private int isMoving; //if isMoving is -1, it is moving left, if 1, it is moving right, else it is not moving
    private bool isJumping;
    private bool isGrounded;

    [SerializeField]
    public float movementSpeed;

    [SerializeField]
    public float jumpForce;

    // Start is called before the first frame update
    private void Start()
    {
        movementController = GetComponent<MovementController>();
        charAnimator = GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Vertical") > 0 && isGrounded)
        {
            isJumping = true;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            isMoving = 1;
        }

        else if (Input.GetAxis("Horizontal") < 0)
        {
            isMoving = -1;
        }

        else
        {
            isMoving = 0;
        }
    }


    private void FixedUpdate()
    {
        if (isJumping)
        {
            movementController.Jump(jumpForce);
            charAnimator.Jump();
            charAnimator.Move(false);
            charAnimator.Ground(false);
            isGrounded = false;
            isJumping = false;
        }

        if (isMoving < 0)
        {
            movementController.MoveLeft(movementSpeed);
            charAnimator.faceLeft();
            if (isGrounded)
            {
                charAnimator.Move();
            }
        }

        else if (isMoving > 0)
        {
            movementController.MoveRight(movementSpeed);
            charAnimator.faceRight();
            if (isGrounded)
            {
                charAnimator.Move();
            }
        }

        else
        {
            movementController.Stationary();
            charAnimator.Move(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (!isGrounded)
            {
                charAnimator.Ground();
            }
            isGrounded = true;
        }
    }
}
