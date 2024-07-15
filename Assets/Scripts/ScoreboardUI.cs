using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreboardUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI AIScoreText;

    private int playerScore = 0;
    private int AIScore = 0;

    void Start() {
        BallMovement.Instance.OnAIScored += BallMovement_OnAIScored;
        BallMovement.Instance.OnPlayerScored += BallMovement_OnPlayerScored;
    }

    private void BallMovement_OnPlayerScored(object sender, EventArgs e) {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
    }

    private void BallMovement_OnAIScored(object sender, EventArgs e) {
        AIScore++;
        AIScoreText.text = AIScore.ToString();
    }
        
}
