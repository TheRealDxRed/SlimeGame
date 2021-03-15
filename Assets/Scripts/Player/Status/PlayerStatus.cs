using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    SpriteRenderer spRen;
    Rigidbody2D rb2D;

    public bool isMirrored, isFloating, invulnerable;
    public float velocityX, velocityY;

    public bool grounded, againstLeft, againstRight, onPlatform, againstFront, againstStep, inWater;
    [SerializeField] public LayerMask whatIsGround, whatIsPlatform, whatIsWall, whatIsWater;
    [SerializeField] public Transform downCheck, backCheck, frontCheck;
    public Vector2 wallFrontSize, wallBackSize;

    Vector2 floorCheckSize = new Vector2(0.55f, 0.1f);
    Vector2 waterCheckSize = new Vector2(0.6f, 0.1f);
    Vector2 stepCheckSize = new Vector2(0.1f, 0.2f);

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
        
        againstStep = Physics2D.OverlapBox(frontCheck.position + new Vector3(0, -0.95f, 0), stepCheckSize, 0, whatIsWall);

        againstLeft = isMirrored ?
            Physics2D.OverlapBox(frontCheck.position, new Vector2(wallFrontSize.x, wallFrontSize.y), 0, whatIsWall) :
            Physics2D.OverlapBox(backCheck.position, new Vector2(wallBackSize.x, wallBackSize.y), 0, whatIsWall);
        againstRight = isMirrored ?
            Physics2D.OverlapBox(backCheck.position, new Vector2(wallBackSize.x, wallBackSize.y), 0, whatIsWall) :
            Physics2D.OverlapBox(frontCheck.position, new Vector2(wallFrontSize.x, wallFrontSize.y), 0, whatIsWall);
        againstFront = Physics2D.OverlapBox(frontCheck.position, new Vector2(0.1f, 1.7f), 0, whatIsWall);
    }

    void OnDrawGizmosSelected() {
        // Yellow
        Gizmos.color = new Color(1, 1, 0, 0.5f);

        // Floor Check
        Gizmos.DrawCube(downCheck.position, new Vector3(floorCheckSize.x, floorCheckSize.y, 1));

        // Step Check
        Gizmos.DrawCube(frontCheck.position + new Vector3(0, -0.95f, 0), new Vector3(stepCheckSize.x, stepCheckSize.y, 1));

        // Back Wall Check
        Gizmos.DrawCube(backCheck.position, new Vector3(wallBackSize.x, wallBackSize.y, 1));

        // Front Wall Check
        Gizmos.DrawCube(frontCheck.position, new Vector3(wallFrontSize.x, wallFrontSize.y, 1));

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
