using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI returnText;
    public TextMeshProUGUI gameOverText;

    [Header("Game Settings")]
    public float gameTime = 60f;
    public float countdownToMenu = 5f;

    private float timeLeft;
    private int score;
    private bool gameOver;

    [Header("Score Animation")]
    public float scorePopScale = 1.5f;   // seberapa besar
    public float popDuration = 0.1f;  // // durasi membesar dan kembali[Header("Loading")]
    public GameObject loadingPrefab;
    private GameObject loadingInstance;

    void Start()
    {
        timeLeft = gameTime;
        score = 0;
        gameOver = false;

        if (resultText != null) resultText.gameObject.SetActive(false);
        if (returnText != null) returnText.gameObject.SetActive(false);
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);

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
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        UpdateTimer();
    }

    public void AddScore(int value)
    {
        if (gameOver) return;

        score += value;
        UpdateScoreUI();
        StartCoroutine(ScorePopCoroutine());

        Debug.Log("Score added: " + value + " | Total: " + score);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void GameOver()
    {
        if (gameOver) return;
        gameOver = true;

        Time.timeScale = 0f;

        if (timerText != null) timerText.text = "TIME UP!";
        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (gameOverText != null) gameOverText.gameObject.SetActive(true);
        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "Score: " + score;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Game Over - Score: " + score);

        StartCoroutine(CountdownToMainMenu());
    }

    IEnumerator CountdownToMainMenu()
    {
        float countdown = countdownToMenu;

        while (countdown > 0)
        {
            if (resultText != null)
                resultText.text = $"Score: {score}";


            if (returnText != null)
            {
                returnText.gameObject.SetActive(true);
                returnText.text = $"Kembali ke Markas dalam waktu {Mathf.CeilToInt(countdown)}...";
            }
            countdown -= Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
        StartCoroutine(LoadMainSceneAsync());

    }
    IEnumerator LoadMainSceneAsync()
    {
        // Tampilkan loading
        if (loadingPrefab != null)
        {
            loadingInstance = Instantiate(loadingPrefab);
            DontDestroyOnLoad(loadingInstance);
        }

        yield return new WaitForSecondsRealtime(0.2f); // kasih waktu render

        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        operation.allowSceneActivation = false;

        // Tunggu sampai loading hampir selesai
        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        // Optional delay biar loading kelihatan
        yield return new WaitForSecondsRealtime(0.5f);

        operation.allowSceneActivation = true;

        // Tunggu sampai scene benar-benar aktif
        while (!operation.isDone)
        {
            yield return null;
        }

        // 🔥 HAPUS LOADING
        if (loadingInstance != null)
        {
            Destroy(loadingInstance);
        }
    }


    private IEnumerator ScorePopCoroutine()
    {
        if (scoreText == null) yield break;

        Vector3 originalScale = scoreText.transform.localScale;
        Vector3 targetScale = originalScale * scorePopScale;

        float timer = 0f;

        // Membesar
        while (timer < popDuration / 2f)
        {
            scoreText.transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / (popDuration / 2f));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        scoreText.transform.localScale = targetScale;
        timer = 0f;

        // Kembali normal
        while (timer < popDuration / 2f)
        {
            scoreText.transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / (popDuration / 2f));
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        scoreText.transform.localScale = originalScale;
    }
}
