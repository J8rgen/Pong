using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BallMovement : MonoBehaviour {

    public static BallMovement Instance { get; private set; }

    public event EventHandler OnAIScored;
    public event EventHandler OnPlayerScored;

    [SerializeField] private float ballInitialSpeed = 10f;
    [SerializeField] private float ballSpeedIncrease = 0.25f;
    [SerializeField] private float maxCenterBallHit = 0.3f;


    private int ballHitCounter;
    private Rigidbody2D rb;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one BallMovement!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        Invoke("BallStart", 2f); // calls BallStart after 2 seconds
    }


    private void FixedUpdate() {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, ballInitialSpeed + (ballSpeedIncrease * ballHitCounter));
    }

    private void BallStart() {

        rb.velocity = new Vector2(-1, 0) * (ballInitialSpeed + ballSpeedIncrease * ballHitCounter);
    }

    private void BallReset() {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        ballHitCounter = 0;
        Invoke("BallStart", 2f); // calls BallStart after 2 seconds
    }

    private void ballBounce(Transform myObject) {
        ballHitCounter++;
        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position; // racket that the ball hit

        float xDirection = transform.position.x > 0 ? -1 : 1;
        float yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;

        if (yDirection == 0) { // hits center
            yDirection = UnityEngine.Random.Range(-maxCenterBallHit, maxCenterBallHit);
        }

        rb.velocity = new Vector2(xDirection, yDirection) * (ballInitialSpeed + (ballSpeedIncrease * ballHitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "PlayerRacket" ||  collision.gameObject.name == "AIRacket") {
            ballBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(transform.position.x > 0) {
            BallReset();
            OnPlayerScored?.Invoke(this, EventArgs.Empty);
        }
        else if (transform.position.x < 0) {
            BallReset();
            OnAIScored?.Invoke(this, EventArgs.Empty);
        }
    }

}
