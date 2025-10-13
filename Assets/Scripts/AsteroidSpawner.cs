using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 3f;
    public int maxAsteroids = 10;
    public Camera cam;
    private float spawnTimer;
    private int currentAsteroids;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0 && currentAsteroids < maxAsteroids)
        {
            SpawnAsteroid();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnAsteroid()
    {
        Vector3 spawnPosition = GetRandomEdgePosition();

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
        if (asteroidScript != null)
        {
            Vector2 directionToCenter = (Vector2)(Vector3.zero - spawnPosition);
            asteroidScript.direction = directionToCenter.normalized;
        }

        currentAsteroids++;
    }

    Vector3 GetRandomEdgePosition()
    {
        int edge = Random.Range(0, 4);
        Vector3 position = Vector3.zero;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        switch (edge)
        {
            case 0:
                position = new Vector3(Random.Range(min.x, max.x), max.y + 1, 0);
                break;
            case 1: 
                position = new Vector3(max.x + 1, Random.Range(min.y, max.y), 0);
                break;
            case 2: 
                position = new Vector3(Random.Range(min.x, max.x), min.y - 1, 0);
                break;
            case 3: 
                position = new Vector3(min.x - 1, Random.Range(min.y, max.y), 0);
                break;
        }

        return position;
    }

    public void AsteroidDestroyed()
    {
        currentAsteroids--;
    }
}