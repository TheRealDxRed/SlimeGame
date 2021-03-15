using UnityEngine;

enum AnimState {
    IDLE,
    WALK,
    FALL,
    JUMP,
    DOUBLEJUMP,
}

// TODO animation frame times
// private readonly int AnimTimes = {  
// };


// TODO implement animations
// TODO(andrew) player assets
public class PlayerAnimation : MonoBehaviour {
    AnimState animationState;
    int animationTimer;
}
