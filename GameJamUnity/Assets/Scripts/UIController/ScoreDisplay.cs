using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int scorePerBite = 200;
    
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerController.OnBite += AddScore;
    }
    
    private void OnDisable()
    {
        PlayerController.OnBite -= AddScore;
    }

    private void AddScore()
    {
        currentScore += scorePerBite;
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
