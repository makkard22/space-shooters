using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public float rotationSpeed = 50f;

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
            GameManager.Instance.SpawnExplosion(other.transform.position);

            other.gameObject.SetActive(false);

            GameManager.Instance.GameOver(transform.position);
            Destroy(gameObject);
        }
    }
}