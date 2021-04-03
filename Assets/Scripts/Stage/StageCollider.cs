using static Globals;
using UnityEngine;

public enum ColliderType {
    NONE,
    FLOOR,
    WALL,
    CEILING,
}

public class StageCollider : MonoBehaviour {
    public ColliderType type = ColliderType.NONE;

    void Awake() {
        switch (type) {
            case ColliderType.FLOOR:
                gameObject.layer = LAYER_TERRAIN;
                break;
            case ColliderType.WALL:
                gameObject.layer = LAYER_WALL;
                break;
            case ColliderType.CEILING:
                gameObject.layer = LAYER_CEILING;
                break;
            default:
                gameObject.layer = 0;
                break;
        }
    }

    void OnDrawGizmosSelected() {
        Color c = Gizmos.color;

        switch (type) {
            case ColliderType.FLOOR:
                Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
                break;
            case ColliderType.WALL:
                Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
                break;
            case ColliderType.CEILING:
                Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
                break;
            default:
                Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                break;
        }

        Gizmos.DrawCube(transform.position, transform.localScale);
        Gizmos.color = c;
    }
}
