using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed;
    [SerializeField] private bool AI;
    [SerializeField] private GameObject pongBall; // for the AI

    private Rigidbody2D rb;
    private Vector2 playerMove;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update() {

        if (AI) {
            AIMove();
        }
        else {
            PlayerMove();
        }
    }

    private void FixedUpdate() { // not dependent on framerate
        rb.velocity = playerMove * movementSpeed;
    }


    private void PlayerMove() {
        if (Input.GetKey(KeyCode.W)) {
            playerMove = new Vector2(0, 1);
        }
        else if (Input.GetKey(KeyCode.S)) {
            playerMove = new Vector2(0, -1);
        }
        else {
            playerMove = Vector2.zero;
        }
    }

    private void AIMove() {

        float AIAccuracy = 0.5f; // 0 is max

        if (pongBall.transform.position.y > transform.position.y + AIAccuracy) {
            playerMove = new Vector2(0, 1);
        }
        else if (pongBall.transform.position.y < transform.position.y - AIAccuracy) {
            playerMove = new Vector2(0, -1);
        }
        else {
            playerMove = new Vector2(0, 0);
        }

    }

}
