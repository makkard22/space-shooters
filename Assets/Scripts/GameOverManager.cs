using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        SetupButtons();
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
        Application.Quit();  // Only compiled in builds
#endif
    }
}