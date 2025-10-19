using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI finalScoreText;  // Drag in Inspector

    void Start()
    {
        Time.timeScale = 1f;
        SetupButtons();
        DisplayFinalScore();
    }

    void DisplayFinalScore()
    {
        int finalScore = PlayerPrefs.GetInt("LastScore", 0);
        Debug.Log($"Loading final score: {finalScore}"); // Debug log

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + finalScore.ToString();
        }
        else
        {
            Debug.LogWarning("Final Score Text is not assigned!");
        }
    }

    void SetupButtons()
    {
        var retryButton = GameObject.Find("RetryButton");
        var quitButton = GameObject.Find("QuitButton");

        if (retryButton != null)
        {
            retryButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(RetryGame);
        }

        if (quitButton != null)
        {
            quitButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(QuitGame);
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}