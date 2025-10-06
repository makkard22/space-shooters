using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Over UI")]
    public Image gameOverImage;

    [Header("Effects")]
    public GameObject explosionPrefab;
    public float explosionDuration = 1f;
    public float delayAfterExplosion = 0.5f; 

    private bool isGameOver = false;

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
        if (gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(false);
            Color c = gameOverImage.color;
            c.a = 0f;
            gameOverImage.color = c;
        }

        Time.timeScale = 1f;
        isGameOver = false;
    }

    public void GameOver(Vector3 spaceshipPosition)
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over!");

        StartCoroutine(GameOverSequence(spaceshipPosition));
    }

    IEnumerator GameOverSequence(Vector3 explosionPosition)
    {
        SpawnExplosion(explosionPosition);

        yield return new WaitForSeconds(explosionDuration);

        yield return new WaitForSeconds(delayAfterExplosion);

        if (gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(true);
            yield return StartCoroutine(FadeInGameOver());
        }

        Time.timeScale = 0f;
    }

    IEnumerator FadeInGameOver()
    {
        float fadeSpeed = 2f;
        Color c = gameOverImage.color;

        while (c.a < 1f)
        {
            c.a += Time.deltaTime * fadeSpeed;
            gameOverImage.color = c;
            yield return null;
        }

        c.a = 1f;
        gameOverImage.color = c;
    }

    public void SpawnExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, position, Quaternion.identity);
        }
    }
}