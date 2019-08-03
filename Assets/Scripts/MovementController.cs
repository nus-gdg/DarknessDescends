using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour {
    public bool MovementEnabled = true;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveLeft(float movementSpeed) {
        if (MovementEnabled) {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
    }

    public void MoveRight(float movementSpeed) {
        if (MovementEnabled) {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
    }

    public void Jump(float jumpForce) {
        if (MovementEnabled) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Stationary() {
        if (MovementEnabled) {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
}