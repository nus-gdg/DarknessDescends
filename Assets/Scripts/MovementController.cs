using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float gravity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity.y;
    }

    public void Move(float movementSpeed, bool isGrounded = true)
    {
        float horizontalVelocity = Input.GetAxis("Horizontal") * movementSpeed;
        rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y + gravity * Time.deltaTime);
        float verticalVelocity = rb.velocity.y;
        if (isGrounded)
        {
            verticalVelocity = 0.0f;
        }
        Vector3 movement = new Vector3(horizontalVelocity, verticalVelocity, 0.0f) * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    public void Jump(float jumpForce)
    {
        rb.AddForce(new Vector2(0.0f, 2.0f) * jumpForce, ForceMode2D.Impulse);
    }
}
