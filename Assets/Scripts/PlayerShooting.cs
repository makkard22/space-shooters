using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject laserPrefab;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        if (laserPrefab != null)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, 0.5f, -1);
            GameObject laser = Instantiate(laserPrefab, spawnPos, transform.rotation);
        }
    }
}