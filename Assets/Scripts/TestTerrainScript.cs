using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTerrainScript : MonoBehaviour {
    [SerializeField]
    public bool semiSolid = false;

    void Start() {
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }
}
