using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    SpriteRenderer spRen;
    Rigidbody2D rb2D;

    public bool isMirrored, isFloating, invulnerable;
    public float velocityX, velocityY;

    public bool grounded, againstLeft, againstRight, onPlatform, inWater;
    [SerializeField] public LayerMask whatIsGround, whatIsPlatform, whatIsWall, whatIsWater;
    [SerializeField] public Transform downCheck, leftCheck, rightCheck;
    public Vector2 wallLeftSize, wallRightSize;

    Vector2 floorCheckSize = new Vector2(0.55f, 0.1f);
    Vector2 waterCheckSize = new Vector2(0.6f, 0.1f);

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        velocityX = rb2D.velocity.x;
        velocityY = rb2D.velocity.y;

        grounded = velocityY < 1f || grounded ? Physics2D.OverlapBox(downCheck.position, floorCheckSize, 0, whatIsGround) : false;
        onPlatform = Physics2D.OverlapBox(downCheck.position, waterCheckSize, 0, whatIsPlatform);

        inWater = Physics2D.OverlapBox(transform.position, waterCheckSize, 0, whatIsWater) && 
                  Physics2D.OverlapBox(downCheck.position, waterCheckSize, 0, whatIsWater);

        againstLeft = Physics2D.OverlapBox(leftCheck.position, new Vector2(wallLeftSize.x, wallLeftSize.y), 0, whatIsWall);
        againstRight =  Physics2D.OverlapBox(rightCheck.position, new Vector2(wallRightSize.x, wallRightSize.y), 0, whatIsWall);
    }

    void OnDrawGizmosSelected() {
        // Yellow
        Gizmos.color = new Color(1, 1, 0, 0.5f);

        // Floor Check
        Gizmos.DrawCube(downCheck.position, new Vector3(floorCheckSize.x, floorCheckSize.y, 1));

        // Back Wall Check
        Gizmos.DrawCube(leftCheck.position, new Vector3(wallLeftSize.x, wallLeftSize.y, 1));

        // Front Wall Check
        Gizmos.DrawCube(rightCheck.position, new Vector3(wallRightSize.x, wallRightSize.y, 1));

        // Green
        Gizmos.color = new Color(0, 1, 0, 0.5f);
    }

    public bool InAir() {
        return (!grounded && !againstLeft && !againstRight);
    }

    public bool AgainstWall() {
        return (againstLeft || againstRight);
    }

    public void Invulnerable(float duration) {
        invulnerable = true;
        Invoke("SetVulnerable", duration);
    }

    void SetVulnerable() {
        invulnerable = false;
    }

    public bool Invulnerable() {
        return invulnerable;
    }
}
