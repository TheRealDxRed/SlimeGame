using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour {
    [SerializeField]
    float speedMax = 5;
    [SerializeField]
    float acceleration = 50;
    [SerializeField]
    int jumpHeight = 15;
    [SerializeField]
    float jumpSpeed = 16;

    Rigidbody2D body;
    Collider2D coll;

    bool jumping = false;
    bool jumpPrev = false;
    int jumpTime = 0;
    float desiredY;
    float distanceToGround;

    Vector2 inputMove;
    bool inputJump;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        distanceToGround = coll.bounds.extents.y;
    }

    void Update() {
    }

    void FixedUpdate() {
        float desiredSpeed = speedMax * inputMove.x;

        if (PressedJump() && IsGrounded()) {
                jumping = true;
                jumpTime = jumpHeight;
        }

        if (jumping) {
            if (body.velocity.y == 0 && jumpTime < jumpHeight) {
                jumping = false;
            } else {
                if (inputJump && jumpTime > 0) {
                    jumpTime--;
                    body.velocity = new Vector2(body.velocity.x, jumpSpeed);
                } else if (inputJump && jumpTime <= 0) {
                    jumping = false;
                } else {
                    body.velocity = new Vector2(body.velocity.x, 0);
                    jumping = false;
                }
            }
        }

        body.AddForce(new Vector2(acceleration * (desiredSpeed - body.velocity.x), 0));

        // Testing stuff, comment out for final game
        if (Vector3.Magnitude(transform.position) > 1000) {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }

    bool IsGrounded() {
        Debug.DrawRay(transform.position, new Vector2(0, -distanceToGround - 0.05f), Color.yellow);

        LayerMask mask = LayerMask.GetMask("Terrain");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanceToGround + 0.05f, mask);

        if (hit.collider != null && hit.collider != coll)
            return true;
        
        return false;
    }

    bool PressedJump() {
        bool pressed = false;

        if (inputJump) {
            if (!jumpPrev)
                pressed = true;
        }

        jumpPrev = inputJump;

        return pressed;
    }

    public void EJumpInput(InputAction.CallbackContext context) {
        inputJump = context.ReadValueAsButton();
    }

    public void EMoveInput(InputAction.CallbackContext context) {
        inputMove = context.ReadValue<Vector2>();
    }
}
