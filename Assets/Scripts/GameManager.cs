using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Effects")]
    public GameObject explosionPrefab;

    [Header("Score System")]
    public int score = 0;
    public int pointsPerAsteroid = 100;

    [Header("UI")]
    public TextMeshProUGUI scoreText;  // For in-game score display

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        score = 0;
        UpdateScoreDisplay();
    }

    public void SpawnExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, position, Quaternion.identity);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
        Debug.Log($"Score: {score}");
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }
}