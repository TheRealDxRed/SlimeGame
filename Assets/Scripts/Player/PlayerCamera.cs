using System.Collections.Generic;
using UnityEngine;

enum CameraState {
    STAY,
    FOLLOW,
    SCROLL,
    RUMBLE,
    CUTSCENE,
}

public class PlayerCamera : MonoBehaviour {
    Rigidbody2D body;

    [SerializeField] GameObject followObject;
    [SerializeField] float followSpeed, rumbleSpeed, cutsceneScrollSpeed;
    [SerializeField] CameraState state = CameraState.FOLLOW;
    [SerializeField] Vector2 scrollSpeed;
    
    Queue<Vector2> cutscenePositions;

    CameraState oldState;

    void Start() {
        body = GetComponent<Rigidbody2D>();

        cutscenePositions = new Queue<Vector2>();

        oldState = state;

        //TODO remove once cutscenes are implemented
        for (int i = 0; i < 10; i++) {
            AddCutscenePosition(new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)));
        }
    }

    void Update() {
        switch (state) {
            case (CameraState.FOLLOW):
                body.velocity = new Vector2(
                    followObject.transform.position.x - transform.position.x,
                    followObject.transform.position.y - transform.position.y
                ) * followSpeed;
                break;
            case (CameraState.SCROLL):
                body.velocity = scrollSpeed;
                break;
            case (CameraState.CUTSCENE):
                //TODO cutscenes
                if (cutscenePositions.Count == 1) {
                    cutscenePositions.Enqueue(cutscenePositions.Peek()); // this feels like a hack, but it *does* prevent a crash
                }

                Vector2 cutsceneNextPosition = cutscenePositions.Peek();

                if (((Vector2)transform.position - cutsceneNextPosition).magnitude < 0.1f) {
                    cutsceneNextPosition = cutscenePositions.Dequeue();
                }

                body.velocity = new Vector2(
                    cutsceneNextPosition.x - transform.position.x,
                    cutsceneNextPosition.y - transform.position.y
                ) * cutsceneScrollSpeed;
                break;
            case (CameraState.RUMBLE):
                body.velocity = new Vector2(
                    followObject.transform.position.x - transform.position.x + Random.Range(-1f, 1f),
                    followObject.transform.position.y - transform.position.y + Random.Range(-1f, 1f)
                    ) * rumbleSpeed;
                break;
            default:
                if (oldState != state)
                    body.velocity = new Vector2(0, 0);
                break;
        }

        oldState = state;
    }

    public void AddCutscenePosition(Vector2 pos) {
        cutscenePositions.Enqueue(pos);
    }

    public void SetAutoscrollSpeed(Vector2 v) {
        scrollSpeed = v;
    }

    public Vector2 GetAutoscrollSpeed() {
        return scrollSpeed;
    }
}
