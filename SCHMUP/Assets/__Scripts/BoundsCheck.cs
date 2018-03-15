using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To type the next 4 lines, start by typing /// and then Tab.
/// <summary>
/// Keeps a GameObject on screen.
/// Note that this ONLY works for an orthographic Main Camera at [ 0, 0, 0 ].
/// </summary>
/// 

public class BoundsCheck : MonoBehaviour {

    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true; // a
    [Header("Set Dynamically")]
    public bool isOnScreen = true; // b
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown; // a

    void Awake() {
        camHeight = Camera.main.orthographicSize; // b
        camWidth = camHeight * Camera.main.aspect; // c
    }

    void LateUpdate() {
        Vector3 pos = transform.position; // c
        isOnScreen = true; // d
        offRight = offLeft = offUp = offDown = false; // b
        if ( pos.x > camWidth - radius) {
            pos.x = camWidth - radius;
            isOnScreen = false; // e
            offRight = true; // c
        }
        if ( pos.x < -camWidth + radius) {
            pos.x = -camWidth + radius;
            isOnScreen = false; // e
            offLeft = true;
        }
        if ( pos.y > camHeight - radius) {
            pos.y = camHeight - radius;
            isOnScreen = false; // e
            offUp = true; // c
        }
        if ( pos.y < -camHeight + radius) {
            pos.y = -camHeight + radius;
            isOnScreen = false; // e
            offDown = true;
        }
        isOnScreen = !(offRight || offLeft || offUp || offDown);
        if ( keepOnScreen && !isOnScreen) { // f
            transform.position = pos; // g
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }
    }

    // Draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos() { // e
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}