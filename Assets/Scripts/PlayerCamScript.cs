using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamScript : MonoBehaviour {
    [SerializeField]
    GameObject followObject;
    [SerializeField]
    float followSpeed;

    Rigidbody2D body;

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 desiredDelta = new Vector2(followObject.transform.position.x - transform.position.x, followObject.transform.position.y - transform.position.y);

        body.velocity = followSpeed * desiredDelta;
    }
}
