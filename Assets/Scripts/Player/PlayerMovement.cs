using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    PlayerStatus status;
    PlayerJump jump;
    Rigidbody2D rb2D;
    //Animator anim;
    [SerializeField] float moveSpeed, acceleration, fallSpeed, wallSlideSpeed;
    bool stunned, wallStickAcquired;

    Vector2 moveInput;

    void Start() {
        status = GetComponent<PlayerStatus>();
        jump = GetComponent<PlayerJump>();
        rb2D = GetComponent<Rigidbody2D>();

        //TODO remove once player inventory is implemented
        wallStickAcquired = false;
    }

    void Update() {
        //TODO player inventory polling
    }

    void FixedUpdate() {
        float desiredSpeed = moveSpeed * moveInput.x;

        //FIXME move wallJumping flag to PlayerStatus to get rid of dependency on PlayerJump
        if (!jump.wallJumping)
            rb2D.AddForce(new Vector2(acceleration * (desiredSpeed - rb2D.velocity.x), 0));

        // Cap fall speed
        if (!status.AgainstWall() && rb2D.velocity.y < -fallSpeed)
            rb2D.velocity = new Vector2(rb2D.velocity.x, -fallSpeed);
        else if (status.AgainstWall() && !wallStickAcquired && rb2D.velocity.y < -wallSlideSpeed)
            rb2D.velocity = new Vector2(rb2D.velocity.x, -wallSlideSpeed);
        else if (status.AgainstWall() && wallStickAcquired && rb2D.velocity.y < 0)
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
    }

    public void MoveInput(Vector2 value) {
        moveInput = value;
    }

    public void Stun(float duration) {
        if (stunned)
            return;

        stunned = true;
        Invoke("RemoveStun", duration);
    }

    void RemoveStun() {
        stunned = false;
    }
}
