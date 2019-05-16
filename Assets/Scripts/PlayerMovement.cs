using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(MovementController))]
public class PlayerMovement : MonoBehaviour
{
    private MovementController movementController;
    private CharacterAnimator charAnimator;

    private bool isGrounded;
    private int x;

    [SerializeField]
    public float movementSpeed;

    [SerializeField]
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<MovementController>();
        charAnimator = GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            movementController.Move(movementSpeed, isGrounded);
            if (isGrounded)
            {
                charAnimator.Move();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            movementController.Jump(jumpForce);
            charAnimator.Jump();
            isGrounded = false;
        }

        else
        {
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
