using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    [SerializeField] private TMP_Text finalScoreText;

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private float startingBallSpeed = 8f;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private float maxBallSpeed = 18f;

    [SerializeField] private GameObject gameOverPanel;

    private float currentBallSpeed;

    private int score;
    [SerializeField] private int startingLives = 3;
    private int lives;

    private void Update()
    {
        if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnEnable()
    {
        Brick.OnBrickDestroyed += AddScore;
        DeathZone.OnBallLost += LoseLife;
    }

    private void OnDisable()
    {
        Brick.OnBrickDestroyed -= AddScore;
        DeathZone.OnBallLost -= LoseLife;
    }

    private void Start()
    {
        lives = startingLives;
        currentBallSpeed = startingBallSpeed;

        gameOverPanel.SetActive(false);

        UpdateScoreUI();
        UpdateLivesUI();

        RespawnBall();
    }

    public void AddScore(int points)
    {
        score += points;

        currentBallSpeed = Mathf.Min(
            currentBallSpeed + speedIncrease,
            maxBallSpeed);

        UpdateScoreUI();
    }

    private void UpdateLivesUI()
    {
        livesText.text = $"Lives: {lives}";
    }

    private void LoseLife()
    {
        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
            return;
        }

        RespawnBall();
    }

    private void GameOver()
    {
        finalScoreText.text = $"Final Score: {score}";
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    private void RespawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);

        ball.GetComponent<BallMove>().SetSpeed(currentBallSpeed);
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }
}