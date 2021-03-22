using UnityEngine;

public class PlayerJump : MonoBehaviour {
    PlayerInventory pinv;
    PlayerStatus status;
    Rigidbody2D rb2D;

    [SerializeField] LayerMask whatIsCeiling;
    [SerializeField] Transform ceilingCheck;
    [SerializeField] Vector2 ceilingCheckSize;
    [SerializeField] float jumpSpeed, doubleJumpSpeed, wallJumpHSpeed, wallJumpVSpeed;
    [SerializeField] int jumpTime, doubleJumpTime, wallJumpTime;

    bool jumping, jumped, doubleJumping, doubleJumped;
    public bool wallJumping;

    float jumpDirection;
    
    int currJumpTime;

    bool jumpInputPrev;

    Vector2 moveInput;
    bool jumpInput;

    void Start() {
        pinv = GetComponent<PlayerInventory>();
        status = GetComponent<PlayerStatus>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // TODO animations
    }

    void FixedUpdate() {
        if (currJumpTime == 0) {
            jumped = jumped || jumping;
            doubleJumped = doubleJumped || doubleJumping;
            jumping = false;
            doubleJumping = false;
            wallJumping = false;
        }

        if (status.inWater) {
            // TODO learn to swim
        } else if (status.InAir()) {
            // JUMP
            if (jumping && jumpInput) {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                currJumpTime--;
            } else if (jumping && !jumpInput) {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                currJumpTime = 0;
            }
            
            // DOUBLEJUMP
            else if (!doubleJumped && jumpInput && !jumpInputPrev && pinv.CanDoubleJump()) {
                // double jump
                doubleJumping = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                currJumpTime = doubleJumpTime;
            } else if (doubleJumping && jumpInput) {
                rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpSpeed);
                currJumpTime--;
            } else if (doubleJumping && !jumpInput) {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                currJumpTime = 0;
            }

            // FIXME weird wall jump behaviour when holding toward wall
            // WALLJUMP LOOP 1
            else if (wallJumping && jumpInput) {
                rb2D.velocity = new Vector2(jumpDirection*wallJumpHSpeed, wallJumpVSpeed);
                currJumpTime--;
            } else if (wallJumping && !jumpInput) {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                currJumpTime = 0;
            }
        } else if (status.grounded) {
            jumped = false;
            doubleJumped = false;
            wallJumping = false;
            jumping = currJumpTime > 0;
            doubleJumping = doubleJumping && currJumpTime > 0;

            // if the player is on the ground and presses jump, jump
            if (jumpInput && !jumpInputPrev) {
                jumping = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                currJumpTime = jumpTime;
            }
        } else if (status.AgainstWall()) {
            doubleJumped = false;
            wallJumping = wallJumping && currJumpTime > 0;

            int againstLeft = status.againstLeft ? 1 : 0;
            int againstRight = status.againstRight ? 1 : 0;
            jumpDirection = againstLeft - againstRight;

            // WALLJUMP START
            if (jumpInput && !jumpInputPrev && pinv.CanWallJump()) {
                wallJumping = true;
                rb2D.velocity = new Vector2(jumpDirection*wallJumpHSpeed, wallJumpVSpeed);
                currJumpTime = wallJumpTime;
            }

            //TODO maybe remove/change this later
            // WALLJUMP LOOP 2
            if (wallJumping) {
                rb2D.velocity = new Vector2(jumpDirection*wallJumpHSpeed, wallJumpVSpeed);
            }
        }

        if (Physics2D.OverlapBox(ceilingCheck.position, ceilingCheckSize, 0, whatIsCeiling) && rb2D.velocity.y > 0) {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
            currJumpTime = 0;
        }

        jumpInputPrev = jumpInput;
    }

    void OnDrawGizmosSelected() {
        // Yellow
        Gizmos.color = new Color(1, 1, 0, 0.5f);

        // Floor Check
        Gizmos.DrawCube(ceilingCheck.position, new Vector3(ceilingCheckSize.x, ceilingCheckSize.y, 1));

        // Green
        Gizmos.color = new Color(0, 1, 0, 0.5f);
    }

    public void JumpInput(bool value) {
        jumpInput = value;
    }

    public void MoveInput(Vector2 value) {
        moveInput = value;
    }
}
