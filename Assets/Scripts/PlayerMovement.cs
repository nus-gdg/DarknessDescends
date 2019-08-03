using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(MovementController))]
public class PlayerMovement : MonoBehaviour {
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
    private void Start() {
        movementController = GetComponent<MovementController>();
        charAnimator = GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetAxisRaw("Vertical") > 0 && isGrounded) {
            isJumping = true;
        }

        if (Input.GetAxisRaw("Horizontal") > 0) {
            isMoving = 1;
        } else if (Input.GetAxisRaw("Horizontal") < 0) {
            isMoving = -1;
        } else {
            isMoving = 0;
        }
    }


    private void FixedUpdate() {
        if (isJumping) {
            movementController.Jump(jumpForce);
            charAnimator.Jump();
            charAnimator.Move(false);
            charAnimator.Ground(false);
            isGrounded = false;
            isJumping = false;
        }

        if (isMoving < 0) {
            movementController.MoveLeft(movementSpeed);
            charAnimator.faceLeft();
            if (isGrounded) {
                charAnimator.Move();
            }
        } else if (isMoving > 0) {
            movementController.MoveRight(movementSpeed);
            charAnimator.faceRight();
            if (isGrounded) {
                charAnimator.Move();
            }
        } else {
            movementController.Stationary();
            charAnimator.Move(false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2[] rays = { new Vector2(0f, 0f), new Vector2(0.5f, 0f), new Vector2(-0.5f, 0f), new Vector2(0.25f, 0f), new Vector2(-0.25f, 0f) };
        int hits = 0;
        foreach (Vector2 displace in rays) {
            RaycastHit2D[] hitArray = new RaycastHit2D[3];
            GetComponent<Collider2D>().Raycast(new Vector2(0f, -1f), hitArray, 1.3f, LayerMask.NameToLayer("Player"));
            if (hitArray[0].collider != null && hitArray[0].collider.tag == "Ground") {
                hits++;
            }
        }
        Debug.Log(hits);
        if (collision.gameObject.tag == "Ground" && hits > rays.Length / 2) {
            if (!isGrounded) {
                charAnimator.Ground();
            }
            isGrounded = true;
        }
    }
}
