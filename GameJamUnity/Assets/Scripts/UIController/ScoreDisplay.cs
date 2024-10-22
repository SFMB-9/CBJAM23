using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] public int currentScore = 0;
    [SerializeField] private int scorePerBite = 200;
    [SerializeField] GameObject finalScore;

    public static Action<int> OnScore;

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
        OnScore?.Invoke(currentScore);
        currentScore += scorePerBite;
        scoreText.text = "Score: " + currentScore.ToString();
        finalScore.GetComponent<TMP_Text>().text = "YOUЯ SCOЯE: " + currentScore.ToString();
    }
    
    public int GetScore()
    {
        return currentScore;
    }
}
