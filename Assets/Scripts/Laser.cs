using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 20f;

    void Start()
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr == null)
            Debug.LogError("No SpriteRenderer found on laser or its children!");
        else
            Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(GameManager.Instance.pointsPerAsteroid);
                GameManager.Instance.SpawnExplosion(other.transform.position);
            }

            AsteroidSpawner spawner = Object.FindAnyObjectByType<AsteroidSpawner>();
            if (spawner != null)
            {
                spawner.AsteroidDestroyed();
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}