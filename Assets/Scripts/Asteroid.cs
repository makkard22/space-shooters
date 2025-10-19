using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public float rotationSpeed = 50f;

    public float explosionAnimationDuration = 2f;

    void Start()
    {
        if (direction == Vector2.zero)
        {
            float angle = Random.Range(0f, 360f);
            direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
                                   Mathf.Sin(angle * Mathf.Deg2Rad));
        }
        direction.Normalize();
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SpawnExplosion(other.transform.position);
            }

            Destroy(other.gameObject);

            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            StartCoroutine(LoadGameOverAfterExplosion());
        }
    }

    IEnumerator LoadGameOverAfterExplosion()
    {
        if (GameManager.Instance != null)
        {
            int currentScore = GameManager.Instance.GetScore();
            PlayerPrefs.SetInt("LastScore", currentScore);
            PlayerPrefs.Save();
            Debug.Log($"Saving score: {currentScore}"); 
        }

        yield return new WaitForSeconds(explosionAnimationDuration);
        SceneManager.LoadScene("GameOverScene");
    }
}