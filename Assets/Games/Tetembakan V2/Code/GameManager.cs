using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshPro timerText;
    public TextMeshPro scoreText;

    [Header("Game Setting")]
    public float gameTime = 60f; // detik

    private float timeLeft;
    private int score;
    private bool gameOver;

    void Start()
    {
        timeLeft = gameTime;
        score = 0;
        gameOver = false;

        UpdateUI();
    }

    void Update()
    {
        if (gameOver) return;

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer();
        }
        else
        {
            timeLeft = 0;
            GameOver();
        }
    }

    void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        UpdateTimer();
    }

    public void AddScore(int value)
    {
        if (gameOver) return;

        score += value;
        scoreText.text = "Score: " + score;
    }

    void GameOver()
    {
        gameOver = true;
        timerText.text = "TIME UP!";
        Debug.Log("Game Over - Score: " + score);
    }
}
